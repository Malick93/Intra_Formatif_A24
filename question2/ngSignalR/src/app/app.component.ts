import { Component } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { MatButtonModule } from '@angular/material/button';


@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
    imports: [MatButtonModule]
})
export class AppComponent {
  title = 'Pizza Hub';

  private hubConnection?: signalR.HubConnection;
  isConnected: boolean = false;

  selectedChoice: number = -1;
  nbUsers: number = 0;

  pizzaPrice: number = 0;
  money: number = 0;
  nbPizzas: number = 0;

  constructor(){
    this.connect();
  }

  connect() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5282/hubs/pizza')
      .build();

    
    // TODO: Mettre isConnected à true seulement une fois que la connection au Hub est faite
    this.hubConnection.start()
        .then(() => {
            this.isConnected = true;
            console.log("connected")
        })
  }

  selectChoice(selectedChoice:number) {
    this.hubConnection!.invoke('SelectChoice', selectedChoice)
    this.selectedChoice = selectedChoice;
  }

  unselectChoice() {
    this.hubConnection!.invoke('UnselectChoice', this.selectedChoice)
    this.selectedChoice = -1;
  }

  addMoney() {
    this.hubConnection!.invoke('AddMoney', this.selectedChoice)
  }

  buyPizza() {
    this.hubConnection!.invoke('BuyPizza', this.selectedChoice)
  }
  async applyEvent(event:any){
    console.log("ApplyingEvent: " + event.eventType)
    switch(event.eventType){
      case "UpdateNbUsers": {
        this.nbUsers = event.UpdateNbUsersEvent;
        break;
      }
      case "UpdateMoney": {
        
        break;
      }
      case "UpdateNbPizzasAndMoney": {

        break;
      }
      case "UpdatePizzaPrice": {

        break;
      }
    }
    if(event.events){
        for(let e of event.events){
            await this.applyEvent(e);
        }
    }
  }
}

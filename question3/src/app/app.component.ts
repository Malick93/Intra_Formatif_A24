import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    standalone: true,
    imports: [
      CommonModule, 
      MatToolbarModule, 
      MatIconModule, 
      MatCardModule, 
      ReactiveFormsModule, 
      MatFormFieldModule, 
      MatInputModule, 
      MatButtonModule
    ]
})
export class AppComponent implements OnInit {
  title = 'reactive.form';
  myForm!: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    // Initialisation du formulaire avec ses validateurs
    this.myForm = this.fb.group({
      name: ['', Validators.required],
      roadnumber: ['', [Validators.required, Validators.min(1000), Validators.max(9999)]],
      rue: [''], // Aucune validation spécifique demandée
      postalcode: ['', Validators.pattern('^[A-Z][0-9][A-Z][ ]?[0-9][A-Z][0-9]$')],
      comments: ['', this.minWordsValidator]
    }, { validators: this.noNameInCommentValidator }); // Validateur au niveau du groupe
  }

  // Validateur personnalisé pour le minimum de 10 mots
  minWordsValidator(control: AbstractControl): ValidationErrors | null {
    if (!control.value) return null; // Le champ est facultatif

    const wordCount = control.value.split(" ").length;
    if (wordCount < 10) {
      return { minWords: true }; // Retourne une erreur si moins de 10 mots
    }
    return null;
  }

  // Validateur personnalisé (croisé) pour vérifier que le nom n'est pas dans le commentaire
  noNameInCommentValidator(group: AbstractControl): ValidationErrors | null {
    const name = group.get('name')?.value;
    const comments = group.get('comments')?.value;

    if (name && comments && comments.includes(name)) {
      return { nameInComment: true };
    }
    return null;
  }
}
import { inject } from '@angular/core';
import { CanActivateFn, createUrlTreeFromSnapshot } from '@angular/router';
import { UserService } from './user.service';

export const guard1: CanActivateFn = (route, state) => {
  let user = localStorage.getItem('user')
  if(user != null){
     if(!inject(UserService).currentUser?.username == JSON.parse(user).username){
      return createUrlTreeFromSnapshot(route, ["/login"])
    }
  }
  else{
    return createUrlTreeFromSnapshot(route, ["/login"])
  }
  return true
};

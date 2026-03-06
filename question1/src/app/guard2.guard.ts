import { inject } from '@angular/core';
import { CanActivateFn, createUrlTreeFromSnapshot } from '@angular/router';
import { UserService } from './user.service';
import { User } from './user';

export const guard2: CanActivateFn = (route, state) => {
  let user = inject(UserService).currentUser
    if(user != null){
      if(!user.prefercat){
        return createUrlTreeFromSnapshot(route, ["/dog"]);
      }
      else{
        return true
      }
    }
    return true;
};

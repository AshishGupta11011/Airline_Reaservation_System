import { Injectable } from '@angular/core';
import { CanActivate, Router} from '@angular/router';
import { AuthenticateService } from '../Authenticate.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authenticate : AuthenticateService, private router : Router){}
  canActivate():boolean{
    if(this.authenticate.Loggedin()){return true}
    else{
      this.router.navigate(['/login']);
      return false;

    }
  }
 
}
  


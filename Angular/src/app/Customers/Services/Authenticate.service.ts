import { Injectable } from '@angular/core';

import {  HttpClient, HttpErrorResponse } from '@angular/common/http';
import {catchError} from "rxjs/operators"
import {throwError} from "rxjs"
import { Router } from '@angular/router';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  customerid :number;

  url :string = 'http://localhost:53981/api/Authenticate';

  constructor(private http : HttpClient ,private router : Router) { }

//to get token
    AuthenticateUser(user : User)
    {
       var response =  this.http.post<any>(this.url,user).pipe(catchError(this.errorHandler));
      // response.subscribe(res => {this.customerid =  res["id"];console.log( res["id"])},
      // error => console.log(error)
      // )
      
       return response;
    }

    errorHandler(error:HttpErrorResponse)
    {
      return throwError(error)
    }
//isLoggedin
    Loggedin(){
      return !!(localStorage.getItem('token') )
      
    }

    getToken(){
      return localStorage.getItem('token');
    }

    getrole()
    {
      return localStorage.getItem('role');
    }

    Logout(){
      localStorage.removeItem('token');
      localStorage.removeItem('id');
      localStorage.removeItem('role');
      this.router.navigate(['/login']);

    }

    Getid(){
      return Number(localStorage.getItem('id'));
    }
  
}

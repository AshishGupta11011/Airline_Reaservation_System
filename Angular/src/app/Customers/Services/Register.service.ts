import { Injectable, ErrorHandler } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http'

import { catchError } from 'rxjs/operators'
import { throwError } from 'rxjs';
import { Customer } from '../Models/Customer';


@Injectable({
  providedIn: 'root'
})
//registerservice
export class RegisterService {
  // urlfake = 'http://localhost:53981/api/Customers/GetCustomerByname'

  url: string = 'http://localhost:53981//api/Customers';

  constructor(private http: HttpClient) { }

  //Create Customer
  AddCustomer(customer: Customer) {

    return this.http.post<any>(this.url, customer).pipe(catchError(this.errorHandler))
  }

  //Get Customer
  GetCustomer() {
    return this.http.get<any>(this.url).pipe(catchError(this.errorHandler))
  }
  
  //Get Customer By Id
  GetCustomerById(id: number) {
    return this.http.get<any>(this.url + `/${id}`).pipe(catchError(this.errorHandler))
  }

  //update Customer
  UpdateCustmomer(id: number, customer: Customer) {
    return this.http.put<any>(this.url + `/${id}`, customer).pipe(catchError(this.errorHandler))
  }

  //Delete Customer
  DeleteCustomer(id: number) {
    return this.http.delete<any>(this.url + `/${id}`).pipe(catchError(this.errorHandler))

  }
  
  //method to get Customer by Email

  // getCustomerByName(name:string)
  // { 
  //   // const options = name ?{ params: new HttpParams().set('name', name) } : {};
  //   const httpOptions = {
  //     headers: new HttpHeaders({

  //       'name': '${this.name}'
  //     })
  //   };
  //   return this.http.get<any>(this.urlfake,httpOptions).pipe(catchError(this.errorHandler))
  // }

  //method to handle errors
  errorHandler(error: HttpErrorResponse) {
    return throwError(error.status);
  }
}

//importing the libraries

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Passenger } from '../Models/passenger.model';

//To inject the depencies in the service
@Injectable({
  providedIn: 'root'
})
export class PassengerService {
  onSearch(searchBookingId: number) {
    return this.http.get<number>('http://localhost:53981/api/PassengersNo' + searchBookingId);
  }

  passengerDetails: Passenger;
////constructor to set the evalues
  constructor(private http: HttpClient) {}
////Base URL for the API

// Method to get all Passengers by calling  /api/Passengers
  getPassenger() {
    return this.http.get<Passenger[]>('http://localhost:53981/api/Passengers');
  }

//Method to get a single passenger by passengerId 
  getPassengerById(pId: string) {
    return this.http.get<Passenger>('http://localhost:53981/api/Passengers/' + pId);
  }

//Method to add the passenger
  addPassenger(passenger: Passenger) {
    return this.http.post('http://localhost:53981/api/Passengers', passenger);
  }

//Method to delete passenger by PassengerId
  deletePassenger(pid: string) {
    return this.http.delete('http://localhost:62850//api/Passengers/' + pid);
  }
//Method to edit passenger by PassengerId
  editPassenger(newPassenger: Passenger, pid: string) {
    return this.http.put('http://localhost:62850//api/Passengers/' + pid, newPassenger);
  }
}
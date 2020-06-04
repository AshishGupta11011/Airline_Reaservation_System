import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from "@angular/forms";
import { Router } from "@angular/router";
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ApiService } from '../../Services/flights.service';
import { Flight } from '../../Models/flight';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {


  //properties
  flight: Flight[];
  addForm: FormGroup;
  constructor(private flightService: ApiService) {
    this.flight = [];
    this.addForm = new FormGroup({
      FlightName: new FormControl("Indigo",[Validators.required, Validators.pattern(/^[a-zA-Z ]{3, 100}$/)]),
      Source: new FormControl("JAI", [Validators.required]),
      Destination: new FormControl("DEL", [Validators.required]),
      BaggageLimit: new FormControl(35,[Validators.required]),
      AvailableSeats: new FormControl(5, [Validators.required]),
      ArrivalTime: new FormControl("2020-05-23T04:05"),
      DepartureTime: new FormControl("2020-05-23T04:05"),
    });

   }

  ngOnInit() {
    
  }

  onSubmit() {
      this.flightService.postFlightsToDatabase(this.addForm.value).subscribe(
        data => window.alert('Flight Successfully Added'),
        error => {
         // var message = this.errorHandler(error);
          window.alert("Unable To addFlight at the moment");
        }
      );

  }
}

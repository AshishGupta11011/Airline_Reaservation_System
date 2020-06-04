import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";
import { ApiService } from '../../Services/flights.service';
import { Flight } from '../../Models/flight';
// import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-view',
  templateUrl: './view.component.html',
  styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {

  flightList: Flight[] = [];

  constructor(private router: Router, private flightService: ApiService) { }

  flightData: Flight[] = [];
  // flightData=[{
  //   "ArrivalTime": "2002-03-02T13:13:13",
  //   "AvailableSeats": "20",
  //   "BaggageLimit": "10",
  //   "DepartureTime": "2000-03-02T11:11:11",
  //   "Destination": "del",
  //   "FlightId": "100",
  //   "FlightName": "AirIndia",
  //   "Source": "ind"
  // },
  // {
  //   "ArrivalTime": "2002-03-02T13:12:13",
  //   "AvailableSeats": "40",
  //   "BaggageLimit": "20",
  //   "DepartureTime": "2001-03-02T11:11:11",
  //   "Destination": "dli",
  //   "FlightId": "101",
  //   "FlightName": "Aier",
  //   "Source": "idg"
  // },{
  //   "ArrivalTime": "2002-03-02T13:13:13",
  //   "AvailableSeats": "20",
  //   "BaggageLimit": "10",
  //   "DepartureTime": "2000-03-02T11:11:11",
  //   "Destination": "del",
  //   "FlightId": "102",
  //   "FlightName": "AirIndia",
  //   "Source": "ind"
  // },
  // {
  //   "ArrivalTime": "2002-03-02T13:12:13",
  //   "AvailableSeats": "40",
  //   "BaggageLimit": "20",
  //   "DepartureTime": "2001-03-02T11:11:11",
  //   "Destination": "dli",
  //   "FlightId": "103",
  //   "FlightName": "Aier",
  //   "Source": "idg"
  // }];

  ngOnInit() {
    console.log("hi");
    this.flightService.getAllFlights().subscribe(flightData => {
      this.flightList = flightData as Flight[];
      this.flightData = this.flightList;
      console.log(this.flightData);
    });
  }

 


add(){
  this.router.navigate(["add"]);
}

delete(FlightId){
  this.flightService.deleteFlight(FlightId).subscribe(deleteResponse => {
    console.log('Delete Response: ' + deleteResponse);
    alert('Flight deleted successfully!');
  });
  console.log(FlightId);
}
flightdata:any;
temp:any = this.flightdata;

// search(value1,value2){
//   this.flightdata = this.temp;

//   console.log(value1)
//   console.log(value2)
//   this.flightData = this.flightData.filter(obj => {
    
//     return obj.Source === value1 && obj.Destination == value2
//   })
//   this.router.navigate(["/search"]);
// }

}

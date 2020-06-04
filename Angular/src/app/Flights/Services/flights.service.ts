import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import {Observable, throwError} from "rxjs/index";
import { Flight } from '../Models/flight';
import { catchError } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class ApiService {
  
 

  constructor(private httpclient: HttpClient) { }
  apiPrefix: string = "http://localhost:53981";

    getAllFlights(): Observable<Flight[]>
    {
      return this.httpclient.get<Flight[]>(this.apiPrefix + "/api/Flight")
    }

    getFlightById(flightid: number): Observable<Flight>
    {
      return this.httpclient.get<Flight>(this.apiPrefix + `/api/Flight?FlightId=${flightid}`);
    }
    postFlightsToDatabase(flight: Flight) {
      return this.httpclient.post<Flight>(this.apiPrefix + "/api/Flight", flight).pipe(catchError(this.errorHandler));;
  
    }
  // Any Error in this contex will be thrown by error handler
  errorHandler(error: HttpErrorResponse) {
    return throwError(error.status)}


    // AddFlight(flight: Flight)
    // {
    //  return this.httpclient.post<Flight>(this.apiPrefix + "/api/Flight", flight);
    
    // }
    
    UpdateFlight(flight:Flight)
    {
    return this.httpclient.put<Flight>(this.apiPrefix + "/api/Flight/UpdateFlight?FlightId=" + flight.FlightId, flight);
    }

    deleteFlight(flightid: number)
     {
    return this.httpclient.delete(this.apiPrefix + "/api/Flight?FlightId=" + flightid, { responseType: "text" });
     }



}

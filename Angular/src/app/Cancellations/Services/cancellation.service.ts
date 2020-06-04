import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class CancellationService {
    deletePassenger(cancellationId: number) {
        return this.http.delete('http://localhost:53981/api/Cancellations/' + cancellationId);
      }
    
    constructor(private http: HttpClient) {}
 
    getCancellationDetails() {
        return this.http.get<any>('http://localhost:53981/api/Cancellations');
    }

    cancelTicket(bId: number) {
        return this.http.get<any>('http://localhost:53981/api/Cancellations/CancellationDetails/' + bId);
    }
}
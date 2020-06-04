import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CancellationService } from '../../Services/cancellation.service';



@Component({
  selector: 'app-user-cancellation',
  templateUrl: './user-cancellation.component.html',
  styleUrls: ['./user-cancellation.component.css']
})
export class UserCancellationComponent implements OnInit {
  bookingId: string;

  constructor(private route: ActivatedRoute, private cancelService: CancellationService) { }

  ngOnInit(): void {
    
    }
    
  onCancelClick(id: number){
    this.bookingId = this.route.snapshot.params['bId'];
    this.cancelService.cancelTicket(Number(this.bookingId)).subscribe(response => {
      console.log(response);
      alert('Ticket Cancelled Successfully !');

  },
  error => {
    window.alert(error);
  }); 


} 
}

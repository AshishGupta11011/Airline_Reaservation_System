import { Component, OnInit } from '@angular/core';
import { UserCancellation } from '../../Models/cancellation.model';
import { CancellationService } from '../../Services/cancellation.service';
import { DatePipe } from '@angular/common';



@Component({
  selector: 'app-admin-cancellation',
  templateUrl: './admin-cancellation.component.html',
  styleUrls: ['./admin-cancellation.component.css']
})
export class AdminCancellationComponent implements OnInit {
  cancellationList: UserCancellation[] ;
  

  constructor(private cancelService: CancellationService, public datePipe: DatePipe) {
    this.cancellationList=[];
   }

  ngOnInit(): void {
  
    this.cancelService.getCancellationDetails().subscribe((cancelData: UserCancellation[]) => {
      this.cancellationList = cancelData;
      console.log(cancelData);
    });
  }

  onDeleteClick(id: number){
    const cancellation = this.cancellationList[id].CancellationId;
    this.cancelService.deletePassenger(cancellation).subscribe(response => {
      console.log(response);
      this.cancellationList = [];
      alert("Cancellation deleted sucessfully");  
    },
    error => {
      alert(error.error.Message);
    });
}
}

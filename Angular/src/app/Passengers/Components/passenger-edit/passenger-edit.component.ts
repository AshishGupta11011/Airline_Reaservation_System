//importing libraries
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import {  FormGroup ,Validators,FormControl} from '@angular/forms';
import { Passenger } from '../../Models/passenger.model';
import { PassengerService } from '../../Services/passenger.service';

@Component({
  selector: 'app-passenger-edit',
  templateUrl: './passenger-edit.component.html',
  styleUrls: ['./passenger-edit.component.css']
})
export class PassengerEditComponent implements OnInit {

   //properties
  passenger: Passenger;
  pId: string;
  //create property for reactive form
  newForm: FormGroup;
 //
  constructor(private route: ActivatedRoute, private router: Router, private service: PassengerService) { }

  ngOnInit(): void {
    
    this.pId = this.route.snapshot.params['pId'];
    console.log(this.pId);

     //create reactive form
     this.newForm=new FormGroup({
        'PassengerId': new FormControl({value: null, disabled: true}, [Validators.required, Validators.min(100)]),
        'BookingId': new FormControl(null, [Validators.required, Validators.min(100)]),
        'EmailId': new FormControl(null, [Validators.email]),
        'Gender': new FormControl(null, [Validators.required, Validators.pattern('^[MFmf]$')]),
        'Name': new FormControl(null, [Validators.required, Validators.pattern('^[A-Za-z .]*$')]),
        'Age': new FormControl(null, [Validators.required, Validators.min(18)]),
        'Nationality': new FormControl(null, [Validators.required, Validators.pattern('^[A-Za-z .]*$')]),
     });

     this.service.getPassengerById(this.pId).subscribe(passengerDetails => {
       this.passenger = passengerDetails as Passenger;
       this.newForm.setValue({
        'PassengerId': this.passenger.PassengerId,
        'BookingId': this.passenger.BookingId,
        'EmailId': this.passenger.EmailId,
        'Gender': this.passenger.Gender,
        'Name': this.passenger.Name,
        'Age': this.passenger.Age,
        'Nationality': this.passenger.Nationality.toLowerCase()
       });
     }); 

  }

  onEditClick(){
        let newPassenger: Passenger = {
          PassengerId: +this.pId,
          BookingId: this.newForm.get('BookingId').value,
          EmailId: this.newForm.get('EmailId').value,
          Gender: this.newForm.get('Gender').value,
          Name: this.newForm.get('Name').value,
          Age: this.newForm.get('Age').value,
          Nationality: this.newForm.get('Nationality').value.substring(0, 3).toUpperCase()
        };
        this.service.editPassenger(newPassenger, this.pId).subscribe(resData => {
          console.log(resData);
        });
        this.newForm.reset();
        this.router.navigate(['/allpassengers']);
   }

}

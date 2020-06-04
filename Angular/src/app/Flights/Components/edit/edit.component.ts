import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';
import { ApiService } from '../../Services/flights.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute, private flightService: ApiService) { }
  EditForm: FormGroup;
  FlightId;
  ngOnInit() {
    this.route.params.subscribe(params => {
      this.FlightId = params.FlightId;
    })

    this.EditForm = this.formBuilder.group({
      FlightId: [],
      FlightName: ['', [Validators.required, Validators.pattern(/^[a-zA-Z ]{3, 100}$/)]],
      Source: ['', Validators.required, Validators.pattern(/^[a-zA-Z ]{3, 100}$/)],
      Destination: ['', Validators.required, Validators.pattern(/^[a-zA-Z ]{3, 100}$/)],
      AvailableSeats: ['', Validators.required, Validators.min(0), Validators.max(500), Validators.pattern(/^[1-9][0-9]+$/)],
      BaggageLimit: ['', Validators.required],
      ArrivalTime: ['', Validators.required],
      DepartureTime: ['', Validators.required]
    });
  }

  // updateform(form){
  //   console.log(form.value)
  // }

  onSubmit() {
    // this.apiService.createUser(this.addForm.value)
    //   .subscribe( data => {
    //     this.router.navigate(['list-user']);
    //   });

    this.EditForm.valid;
    this.EditForm.markAllAsTouched();
    if (this.EditForm.invalid) {
      console.log("please enter all values")
      return;
    }
    console.log(this.EditForm.value);
    this.flightService.UpdateFlight(this.EditForm.value).subscribe(updateResponse => {
      console.log(updateResponse);
      alert('Flight updated successfully');
    },
    error => {
      console.log(error.error.Message);
    });
    console.log("yha se nevigation");
    this.router.navigate(['/']);
  }

}

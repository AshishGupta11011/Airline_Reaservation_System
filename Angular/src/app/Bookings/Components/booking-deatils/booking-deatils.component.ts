// //***************************************************************************************
// //Developer: <Ashita Gaur>
// //Create Date: <17th May,2020>
// //Last Updated Date: <21th May,2020>
// //Description:To perform Business logic and accordingly return response to Bookings.
// //Task:To create methods, properties ,validations and 
// //***************************************************************************************


import { Component, OnInit, Input, HostListener, ViewChild, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Booking } from '../../Models/booking';
import { BookingDataService } from '../../Services/booking-data.service';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-booking-deatils',
  templateUrl: './booking-deatils.component.html',
  styleUrls: ['./booking-deatils.component.css']
})
export class BookingDeatilsComponent implements OnInit {


  @ViewChild('bookingSuccessModal') bookingSuccessModal;
  // taking current date data
  todayDate: Date = new Date();

  // flight class drop down menu data
  flightClassEntries = [{
    name: "First Class",
    value: "F"
  },
  {
    name: "Business Class",
    value: "B"
  },
  {
    name: "Economy Class",
    value: "E"
  }]


  // data for city drop down menu 
  cityData = [{
    name: "Jaipur",
    value: "JAI"
  },
  {
    name: "Delhi",
    value: "DEL"
  },
  {
    name: "Pune",
    value: "PUN"
  },
  {
    name: "Mumbai",
    value: "MUM"
  },
  {
    name: "Kolkata",
    value: "KOL"
  }]

  //properties 
  bookings: Booking[];
  searchBookingId: number;


  //property for reactive form 
  newForm: FormGroup;



  //Filter source and destination drop down menu 
  filterDestArgs: boolean;
  filterSourceArgs: boolean;
  updateForm: FormGroup;
  isUpdateFormActive: boolean = false;
  activeTabIndex: number = 1;
  bookingIdForModal: any;
  // filterArgs: String = '';


  //constructor
  constructor(private bookingsService: BookingDataService, private router: Router, @Inject(DOCUMENT) private document: Document) {

    //bookings object aaray 
    this.bookings = [];

    //reactive form for updated booking data
    this.updateForm = new FormGroup({
      BookingId: new FormControl(0),
      Class: new FormControl(this.flightClassEntries[0].value),
      Source: new FormControl("", [Validators.required]),
      Destination: new FormControl("", [Validators.required]),
      DateOfBooking: new FormControl(this.todayDate),
      DateOfJourney: new FormControl(this.todayDate, [Validators.required]),
      NoOfSeats: new FormControl(1, [Validators.max(5), Validators.min(1)]),
    });


    //reactive form for booking data
    this.newForm = new FormGroup({
      Class: new FormControl(this.flightClassEntries[0].value),
      Source: new FormControl("", [Validators.required]),
      Destination: new FormControl("", [Validators.required]),
      DateOfBooking: new FormControl(this.todayDate),
      DateOfJourney: new FormControl(this.todayDate, [Validators.required]),
      NoOfSeats: new FormControl(1, [Validators.max(5), Validators.min(1)]),
    });
  }

  //document click listener to close autocomplete when click outside
  @HostListener('document:click', ['$event']) onMouseEnter(event) {
    // console.log(event.target.id);
    if (event.target.id != "sourceSearch") {
      this.filterSourceArgs = this.filterSourceArgs ? false : false;
    }
    else if (event.target.id != "destSearch") {
      this.filterDestArgs = this.filterDestArgs ? false : false;
    }
  }


  ngOnInit() {
  }

  //open update box when click
  updateClickHandler() {
    this.isUpdateFormActive = true;
  }

  //city code fetch from city name
  updateCityData(of: string, data) {
    if (of == 'from') {
      this.newForm.controls.Source.setValue(data.value);
    }
    else {
      this.newForm.controls.Destination.setValue(data.value);
    }
    this.autoCompleteClose();
  }

  //auto complete dropdown close function
  autoCompleteClose() {
    this.filterSourceArgs = this.filterDestArgs = false;
  }

  //filter args handler to function auto complete dropdown
  setArgsTrue(of: string) {
    if (of == 'from') {
      this.filterSourceArgs = true;
      this.filterDestArgs = false;
    }
    else {
      this.filterSourceArgs = false;
      this.filterDestArgs = true;
    }
  }

  //Create a Booking Deatils to Database
  onSubmitClick() {
    if (this.newForm.valid == true) {
      //accessing value of any form control (textbox etc.)
      // console.log('Form Value: ' + this.newForm.value);

      this.bookingsService.postBookingsToDatabase(this.newForm.value).subscribe(
        data => {
          this.bookingIdForModal = data;
          this.document.body.classList.add('modal-open');
          // this.bookingSuccessModal.open();
        },
        error => {
          var message = this.errorHandler(error);
          window.alert(message);
        }
      );
      //clear textboxes
      this.newForm.reset();
      this.newForm.markAsDirty();
      this.newForm.controls.DateOfBooking.setValue(this.todayDate);
      this.newForm.controls.Class.setValue(this.flightClassEntries[0].value);
      this.newForm.controls.NoOfSeats.setValue(1);
    }
    else {
      console.log("Invalid data");
    }
  }


  //Finding flight Status by booking id
  onFindStatusClick() {
    // console.log(this.searchBookingId);
    this.bookingsService.getBookingsById(this.searchBookingId).subscribe(data => {
      var obj: any
      obj = data as Booking[];
      if (obj.length) {
        this.bookings = obj;
      }
      else {
        this.bookings = [];
        this.bookings.push(obj);
      }
      this.updateForm.controls.BookingId.setValue(this.bookings[0].BookingId ? this.bookings[0].BookingId : 100);
      this.updateForm.controls.Class.setValue(this.bookings[0].Class ? this.bookings[0].Class : 'F');
      this.updateForm.controls.NoOfSeats.setValue(this.bookings[0].NoOfSeats ? this.bookings[0].NoOfSeats : 1);
      this.updateForm.controls.DateOfJourney.setValue(this.bookings[0].DateOfJourney ? this.bookings[0].DateOfJourney : this.todayDate);
      this.updateForm.controls.Destination.setValue(this.bookings[0].Destination ? this.bookings[0].Destination : 'DEL');
      this.updateForm.controls.Source.setValue(this.bookings[0].Source ? this.bookings[0].Source : 'JAI');
    },
      error => console.log(error));
  }

  // button disable if booking id is less then 100
  checkButton() {
    if (this.searchBookingId == undefined && this.searchBookingId < 100) {
      return true;
    }
    else
      return false;
  }

  // getting  all booking data from database
  onDetailsClick() {
    //  To navigate to the bookings page
    //this.router.navigate(['/booking']);
    this.bookingsService.getBookingsFromDatabase().subscribe(data => {
      this.bookings = data as Booking[];
      console.log('Data: ' + data);
      console.log(this.bookings);
    },
      error => console.log(error));
    console.log("Getting data from database");
  }
  onClassChange(event) {
    this.newForm.controls.Class.setValue(event);
    console.log(this.newForm.controls.Class.value);
  }

  // update a booking data by booking id
  onUpdateBookingClick() {
    if (this.updateForm.valid == true) {
      //accessing value of any form control (textbox etc.)
      this.bookingsService.updateBookingsToDatabase(this.updateForm.value.BookingId, this.updateForm.value).subscribe(
        data => console.log(data),
        error => console.log(error)
      );
      //clear textboxes
      this.updateForm.reset();
      this.isUpdateFormActive = false;
      this.bookings = [];
      this.updateForm.controls.DateOfBooking.setValue(this.todayDate);
      this.updateForm.controls.DateOfJourney.setValue(this.todayDate);
      this.updateForm.controls.Class.setValue(this.flightClassEntries[0].value);
      this.updateForm.controls.NoOfSeats.setValue(1);
    }
    else {
      console.log("Invalid data");
    }

  }

  // tab index updater to select a tab
  updateTabIndex(value) {
    this.activeTabIndex = value;
    if (value == 2 || value == 3) {
      this.searchBookingId = 100;
      this.bookings = [];
    }
  }


  //errror handling function
  errorHandler(statusCode) {
    var message = '';
    switch (statusCode) {
      case 500:
        message = 'Currently flight is not available for this route. Please try for another route .';
        break;
      case 400: {
        message = 'Server Unreachable';
        break;
      }
      default: {
        message = 'Something Wrong Happened';
        break;
      }
    }
    return message;
  }

  // modal function to close modal
  modalClose() {
    // window
    this.document.body.classList.remove('modal-open');
    this.bookingIdForModal = 0;
  }

  //Aniket's Cancel Ticket code
  // bookingId: number;
  // onCancelClick(id: number) {
  //   // this.bookingId = this.route.snapshot.params['bId'];
  //   this.bookingId = this.searchBookingId;
  //   this.cancelService.cancelTicket(this.bookingId).subscribe(response => {
  //     console.log(response);
  //     window.alert('Ticket Cancelled Successfully !');

  //   },
  //     error => {
  //       window.alert(error.error.Message);
  //     });
  // }
  // //Anikets code END
}


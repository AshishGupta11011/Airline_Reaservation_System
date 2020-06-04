import { Component, OnInit } from '@angular/core';
import { Customer } from '../../Models/Customer';
import { RegisterService } from '../../Services/Register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-of-customers',
  templateUrl: './list-of-customers.component.html',
  styleUrls: ['./list-of-customers.component.css']
})
export class ListOfCustomersComponent implements OnInit {

  customerList : Customer[];

  constructor(private register : RegisterService,private router : Router) {
    this.customerList= [];
   }

  ngOnInit(): void {
  

    this.register.GetCustomer().subscribe(
      res => {this.customerList = res},
      error => {console.log(error);}
    )
  }

}

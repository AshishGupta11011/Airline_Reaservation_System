import { Component, OnInit } from '@angular/core';

import { error } from 'protractor';
import { Router } from '@angular/router';
import { Customer } from '../../Models/Customer';
import { RegisterService } from '../../Services/Register.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  customerList : Customer[];

  isAdmin : boolean = false;

  constructor(private register : RegisterService,private router : Router) {
    this.customerList= [];
   }

  ngOnInit(): void {
  

    this.register.GetCustomer().subscribe(
      res => {this.isAdmin = true;console.log("user is admin");},
      error => {console.log(error);this.router.navigate(['/booking']);window.alert("Only For Admin");}
    )
  }

}

//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  profile.component.cs
// Create Date  :  <17th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  Gets the customer details And user can update their details And delete their Account
// ------------------------------------------------------------------------------------------

import { Component, OnInit } from '@angular/core';
import { strict } from 'assert';
import { Customer } from '../../Models/Customer';
import { RegisterService } from '../../Services/Register.service';
import { AuthenticateService } from '../../Services/Authenticate.service';



@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  id: number;

  customerModel: Customer = new Customer();

  constructor(private custservice: RegisterService, private authenticate: AuthenticateService) { }

    today = new Date().setFullYear(new Date().getFullYear() - 10);
   
   
   
    ngOnInit(): void {
    this.id = this.authenticate.customerid;
    this.custservice.GetCustomerById(this.id).subscribe(
      req => { this.customerModel = req; },
      error => { console.log(error) }

      
    )

  }

  onUpdate() {

    this.custservice.UpdateCustmomer(this.id, this.customerModel)
      .subscribe(

        req => { console.log(req); window.alert("Customer Updated") },
        error => { console.log(error); }
      )
  }

  onDelete() {
    this.custservice.DeleteCustomer(this.id).subscribe(
      req => {
        console.log(req);
        window.alert("Account Deleted");
        this.authenticate.Logout();
      },
      error => window.alert("Unable to delete Account!!\n Try after some time")
    )
  }

}

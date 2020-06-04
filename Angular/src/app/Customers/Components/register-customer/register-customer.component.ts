//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  register-component.component.cs
// Create Date  :  <17th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  registers the customer  
// ------------------------------------------------------------------------------------------

import { Component, OnInit } from '@angular/core';
import { error } from 'protractor';
import { Route } from '@angular/compiler/src/core';
import { Router } from '@angular/router';
import { Customer } from '../../Models/Customer';
import { RegisterService } from '../../Services/Register.service';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.css']
})
export class RegisterCustomerComponent implements OnInit {
  public EmailAlreadyExists: boolean = false;
  Registered: boolean = false;

  customerModel: Customer = new Customer();
  constructor(private custservice: RegisterService , private router : Router ) { }



  ngOnInit(): void {
  }

  CreateCust() {
//send the customer details for account to be created to the  service
    this.custservice.AddCustomer(this.customerModel)
      .subscribe(
        
        data => { 
          
          if(data == 409){window.alert('Email Already Exists')};
          if(data == 201){this.Registered = true;window.alert('User Account Created')};
           console.log("Success",data) 

        },error => {console.log(error)  }
         
      )

      if(this.Registered){
        this.router.navigate(['login']);
      }
  }


}






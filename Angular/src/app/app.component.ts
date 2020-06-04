import { Component, OnInit } from '@angular/core';
import { AuthenticateService } from './Customers/Services/Authenticate.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  role:string  ;
  title = 'app works';
  constructor(public authenticate : AuthenticateService){}
ngOnInit(){
  console.log("helloo");
  console.log(this.authenticate.Getid());
if(this.authenticate.customerid == null && this.authenticate.Getid() != null)
{
this.authenticate.customerid = Number(this.authenticate.Getid());
}
}
  }
   

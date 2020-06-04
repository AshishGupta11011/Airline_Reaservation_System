import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms'
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { RouterModule } from '@angular/router';
import { PassengerComponent } from './Passengers/Components/passenger/passenger.component';
import { PassengerListComponent } from './Passengers/Components/passenger-list/passenger-list.component';
import { SafePipe } from './Bookings/pipes/safe/safe.pipe';
import { BookingDeatilsComponent } from './Bookings/Components/booking-deatils/booking-deatils.component';
import { PassengerService } from './Passengers/Services/passenger.service';
import { RegisterService } from './Customers/Services/Register.service';
import { AuthenticateService } from './Customers/Services/Authenticate.service';
import { AuthGuard } from './Customers/Services/Guard/auth.guard';
import { TokenInterceptorService } from './Customers/Services/Interceptor/token-interceptor.service';
import { LoginCustomerComponent } from './Customers/Components/login-customer/login-customer.component';
import { RegisterCustomerComponent } from './Customers/Components/register-customer/register-customer.component';

import { AdminComponent } from './Customers/Components/admin/admin.component';
import { PageNotFoundComponent } from './Customers/Components/page-not-found/page-not-found.component';
import { ProfileComponent } from './Customers/Components/profile/profile.component';
import { AdminCancellationComponent } from './Cancellations/Components/admin-cancellation/admin-cancellation.component';

import { UserCancellationComponent } from './Cancellations/Components/user-cancellation/user-cancellation.component';
import { CancellationService } from './Cancellations/Services/cancellation.service';
import { PassengerEditComponent } from './Passengers/Components/passenger-edit/passenger-edit.component';
import { ListOfCustomersComponent } from './Customers/Components/list-of-customers/list-of-customers.component';
import { FilterCityPipe } from './Bookings/pipes/filter-city/filter-city.pipe';
import { BookingDataService } from './Bookings/Services/booking-data.service';
import { AddComponent } from './Flights/Components/add/add.component';
import { EditComponent } from './Flights/Components/edit/edit.component';
import { ViewComponent } from './Flights/Components/view/view.component';
import { SearchFlightComponent } from './Flights/Components/search-flight/search-flight.component';
import { DatePipe } from '@angular/common';




@NgModule({
  declarations: [
    AppComponent,
    SafePipe,
    FilterCityPipe,
    SafePipe,
    BookingDeatilsComponent,
    AppComponent,
    LoginCustomerComponent,
    RegisterCustomerComponent,

    AddComponent,
    EditComponent,
    ViewComponent,
    
    AdminComponent,
    PageNotFoundComponent,
    ProfileComponent,
    AdminCancellationComponent,
    UserCancellationComponent,
   PassengerComponent,
   PassengerListComponent,
   PassengerEditComponent,
   ListOfCustomersComponent,
   SearchFlightComponent,

    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    
    
  ],
  providers: [PassengerService,DatePipe, BookingDataService,RegisterService, AuthenticateService,AuthGuard,CancellationService,PassengerService,
    {provide: HTTP_INTERCEPTORS,
    useClass : TokenInterceptorService,
  multi : true} ],

  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }

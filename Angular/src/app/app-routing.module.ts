import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PassengerComponent } from './Passengers/Components/passenger/passenger.component';
import { PassengerListComponent } from './Passengers/Components/passenger-list/passenger-list.component';
import { AuthGuard } from './Customers/Services/Guard/auth.guard';
import { AdminComponent } from './Customers/Components/admin/admin.component';
import { LoginCustomerComponent } from './Customers/Components/login-customer/login-customer.component';
import { RegisterCustomerComponent } from './Customers/Components/register-customer/register-customer.component';
import { ProfileComponent } from './Customers/Components/profile/profile.component';
import { PageNotFoundComponent } from './Customers/Components/page-not-found/page-not-found.component';
import { AdminCancellationComponent } from './Cancellations/Components/admin-cancellation/admin-cancellation.component';
import { UserCancellationComponent } from './Cancellations/Components/user-cancellation/user-cancellation.component';
import { PassengerEditComponent } from './Passengers/Components/passenger-edit/passenger-edit.component';
import { ListOfCustomersComponent } from './Customers/Components/list-of-customers/list-of-customers.component';
import { BookingDeatilsComponent } from './Bookings/Components/booking-deatils/booking-deatils.component';
import { AddComponent } from './Flights/Components/add/add.component';
import { ViewComponent } from './Flights/Components/view/view.component';
import { EditComponent } from './Flights/Components/edit/edit.component';
import { SearchFlightComponent } from './Flights/Components/search-flight/search-flight.component';


const routes: Routes = [
    //ashish
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    // { path: 'booking', component: BookingComponent, canActivate: [AuthGuard] },
    { path: 'booking',component: BookingDeatilsComponent },
    { path: 'admin', component: AdminComponent ,canActivate: [AuthGuard]},
    { path: 'login', component: LoginCustomerComponent },
    { path: 'Register', component: RegisterCustomerComponent },
    { path: 'Profile/:id', component: ProfileComponent },
    {path : 'allpassengers' , component: PassengerListComponent,canActivate: [AuthGuard]},
    {path: 'editpassenger/:pId', component : PassengerEditComponent},
    {path: 'Customerslist', component : ListOfCustomersComponent,canActivate: [AuthGuard]},
    {path: 'addflight', component: AddComponent,canActivate: [AuthGuard]},
    {path: 'viewflight', component:ViewComponent,canActivate: [AuthGuard]},
    {path: 'editflight', component:EditComponent,canActivate: [AuthGuard]},
    
    {path: 'searchflight', component:SearchFlightComponent},
  // { path: '', redirectTo: 'AddPassenger', pathMatch: 'full' },
  { path: 'AddPassenger', component: PassengerComponent },
  {path: 'Cancellationsadmin',component: AdminCancellationComponent},
  {path: 'Cancellationsuser' ,component: UserCancellationComponent },
  { path: '**', component: PageNotFoundComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

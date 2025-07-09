import { Routes } from '@angular/router';
import { LoginComponent } from './components/User comp/login/login.component';
import { RegisterComponent } from './/components/User comp/register/register.component';
import { AdminComponent } from './components/User comp/admin/admin.component';
import { MyTravelsComponent } from './components/Travel comp/my-travels/my-travels.component';


export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'admin', component: AdminComponent },
  { path: 'my-travels/:id', component: MyTravelsComponent },
  {
    path: 'destinations',
    loadComponent: () =>
      import('./components/Place comp/place/place.component').then(m => m.PlacesComponent)
  },
  {
    path: 'flights',
    loadComponent: () =>
      import('./components/Flight comp/flight/flight.component').then(m => m.FlightComponent)
  },
  {
    path: 'admin-travels',
    loadComponent: () =>
      import('./components/Travel comp/admin-travel/admin-travel.component').then(m => m.AdminTravelsComponent)
  },
  {
    path: 'order-flight',
    loadComponent: () =>
      import('./components/Flight comp/order-flight/order-flight.component').then(m => m.OrderFlightComponent)
  },
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];

import { Component } from '@angular/core';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';


@Component({
  selector: 'app-admin',
  imports: [NavbarComponent,HeaderComponent],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.css'
})
export class AdminComponent {

}

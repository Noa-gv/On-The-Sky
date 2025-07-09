import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { TravelService } from '../../../Services/travel.service';
import Travel from '../../../Models/travel.Model';

interface TravelWithStatus extends Travel {
  isPast: boolean;
}

@Component({
  selector: 'app-my-travels',
  standalone: true,
  imports: [CommonModule, NavbarComponent, HeaderComponent],
  templateUrl: './my-travels.component.html',
  styleUrls: ['./my-travels.component.css']
})
export class MyTravelsComponent implements OnInit {
  travels: TravelWithStatus[] = [];

  constructor(private travelService: TravelService) {}

  ngOnInit(): void {
    const now = new Date();

    this.travelService.getMyTravels().subscribe(travels => {
      this.travels = travels.map(travel => {
        const isPast = new Date(travel.flight.flighttime) < now;
        return { ...travel, isPast };
      });
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { TravelService } from '../../../Services/travel.service';
import Travel from '../../../Models/travel.Model';
import { TravelItemComponent } from '../travel-item/travel-item.component';

@Component({
  selector: 'app-admin-travels',
  standalone: true,
  imports: [CommonModule, TravelItemComponent, NavbarComponent, HeaderComponent],
  templateUrl: './admin-travel.component.html',
  styleUrls: ['./admin-travel.component.css'],
})
export class AdminTravelsComponent implements OnInit {
  travels: Travel[] = [];

  constructor(private travelService: TravelService) {}

  ngOnInit(): void {
    this.travelService.getAll().subscribe((travels: Travel[]) => {
      this.travels = travels;
    });
  }

  onApprove(travel: Travel) {
    travel.status = 'approved';
    alert(`אישרת את נסיעה ${travel.travelId}`);
  }

  onReject(travel: Travel) {
    travel.status = 'rejected';
    alert(`דחית את נסיעה ${travel.travelId}`);
  }
}

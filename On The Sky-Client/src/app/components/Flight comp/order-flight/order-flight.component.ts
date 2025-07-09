import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { TravelService } from '../../../Services/travel.service';
import { FlightService } from '../../../Services/flight.service';
import Travel from '../../../Models/travel.Model';
import Flight from '../../../Models/flight.Model';

@Component({
  selector: 'app-order-flight',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent, HeaderComponent],
  templateUrl: './order-flight.component.html',
  styleUrls: ['./order-flight.component.css']
})
export class OrderFlightComponent implements OnInit {
  travel: Partial<Travel> = {
    flightId: 0,
    amountTickets: 1
  };

  flights: Flight[] = [];
  travels: Travel[] = [];
  selectedTravelId: number | null = null;

  constructor(
    private travelService: TravelService,
    private flightService: FlightService
  ) { }

  ngOnInit(): void {
    this.loadFlights();
    this.loadTravels(); 
  }

  loadFlights() {
    const now = new Date();
    this.flightService.getFlights().subscribe(flights => {
      this.flights = flights
        .filter(f => new Date(f.flighttime) >= now)
        .sort((a, b) => new Date(a.flighttime).getTime() - new Date(b.flighttime).getTime());
    });
  }

  loadTravels() {
    const now = new Date();
    this.travelService.getMyTravels().subscribe(travels => {
      this.travels = travels
        .filter(t => new Date(t.flight.flighttime) >= now)
        .sort((a, b) => new Date(a.flight.flighttime).getTime() - new Date(b.flight.flighttime).getTime());
    });
  }

  submitOrder() {
    const newTravel = {
      flightId: this.travel.flightId!,
      amountTickets: this.travel.amountTickets!
    };

    this.travelService.add(newTravel as Travel).subscribe(() => {
      alert('The order was submitted successfully!');
      this.clearForm();
      this.loadTravels(); 
    });
  }

  selectForEdit(travel: Travel) {
    this.selectedTravelId = travel.travelId;
    this.travel = {
      flightId: travel.flightId,
      amountTickets: travel.amountTickets
    };
  }

  updateTravel() {
    if (this.selectedTravelId === null) return;

    const updatedTravel = {
      flightId: this.travel.flightId!,
      amountTickets: this.travel.amountTickets!
    };

    this.travelService.update(this.selectedTravelId, updatedTravel as Travel).subscribe(() => {
      alert('Order updated successfully!');
      this.clearForm();
      this.loadTravels(); 
    });
  }

  delete(id: number) {
    if (confirm('Are you sure you want to delete this order?')) {
      this.travelService.delete(id).subscribe(() => {
        alert('The order has been deleted.');
        this.loadTravels(); 
      });
    }
  }

  clearForm() {
    this.travel = { flightId: 0, amountTickets: 1 };
    this.selectedTravelId = null;
  }
}

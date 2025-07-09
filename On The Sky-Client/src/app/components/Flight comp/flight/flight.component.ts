import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ValidatorFn, AbstractControl, ValidationErrors, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { FlightService } from '../../../Services/flight.service';
import { AuthService } from '../../../Services/auth.service';
import { PlaceService } from '../../../Services/place.service';
import Flight from '../../../Models/flight.Model';
import Place from '../../../Models/place.Model';
import {PrettyDatePipe} from '../../../Directive & Pipe/pretty-date.pipe'

@Component({
  selector: 'app-flights',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf, NgFor, FormsModule, NavbarComponent, HeaderComponent,PrettyDatePipe],
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent implements OnInit {
  flights: Flight[] = [];
  filteredFlights: Flight[] = [];
  places: Place[] = [];
  searchText: string = '';

  isManager = false;
  form: FormGroup;
  editingId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private flightService: FlightService,
    private authService: AuthService,
    private placeService: PlaceService
  ) {
    this.form = this.fb.group({
      flighttime: ['', [Validators.required, this.futureDateValidator]],
      countryID: ['', Validators.required],
      maximum: ['', [Validators.required, Validators.min(1)]],
      amount: [0, [Validators.required, Validators.min(0)]],
      price: ['', [Validators.required, Validators.min(0)]],
    }, {
      validators: this.validateAmountLessThanMax()
    });
  }

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.isManager = user?.role === 'manager';
      this.loadFlights();
    });

    this.placeService.getAll().subscribe(p => {
      this.places = p;
    });
  }

  loadFlights() {
    this.flightService.getFlights().subscribe(flights => {
      this.flights = flights;

      if (this.isManager) {
        this.filteredFlights = [...this.flights];
      } else {
        const now = new Date();
        this.filteredFlights = this.flights.filter(f =>
          new Date(f.flighttime) > now &&
          f.amount < f.maximum
        );
      }
    });
  }

  onSearchChange() {
    const lower = this.searchText.toLowerCase();
    this.filteredFlights = this.flights.filter(f =>
      f.country.country.toLowerCase().includes(lower) ||
      f.flighttime.toString().toLowerCase().includes(lower)
    );
  }

  startEdit(flight: Flight) {
    this.editingId = flight.flightid;
    const dateObj = new Date(flight.flighttime);

    this.form.patchValue({
      flighttime: dateObj.toISOString().substring(0, 16),
      countryID: flight.countryID,
      maximum: flight.maximum,
      amount: flight.amount,
      price: flight.price
    });
  }

  cancelEdit() {
    this.editingId = null;
    this.form.reset({
      flighttime: '',
      countryID: '',
      maximum: '',
      amount: 0,
      price: '',
    });
  }

  save() {
    if (this.form.invalid) return;
    const value = this.form.value;

    value.flighttime = new Date(value.flighttime);

    if (!this.editingId) {
      value.amount = 0;
    }

    if (this.editingId) {
      this.flightService.update(this.editingId, value).subscribe(() => {
        this.cancelEdit();
        this.loadFlights();
      });
    } else {
      this.flightService.add(value).subscribe(() => {
        this.cancelEdit();
        this.loadFlights();
      });
    }
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.flightService.delete(id).subscribe(() => this.loadFlights());
    }
  }

  futureDateValidator(control: AbstractControl) {
    const value = new Date(control.value);
    return value > new Date() ? null : { pastDate: true };
  }

  validateAmountLessThanMax(): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const max = group.get('maximum')?.value;
      const amt = group.get('amount')?.value;
      return amt <= max ? null : { amountTooHigh: true };
    };
  }

  get flighttime() {
    return this.form.get('flighttime');
  }
  get countryID() {
    return this.form.get('countryID');
  }
  get maximum() {
    return this.form.get('maximum');
  }
  get amount() {
    return this.form.get('amount');
  }
  get price() {
    return this.form.get('price');
  }

  // הפונקציה החדשה שבודקת אם טיסה כבר עברה
  isFlightPassed(flight: Flight): boolean {
    return new Date(flight.flighttime) <= new Date();
  }
}

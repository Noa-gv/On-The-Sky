import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule, NgIf, NgFor } from '@angular/common';
import { NavbarComponent } from '../../shared/navbar/navbar.component';
import { HeaderComponent } from '../../shared/header/header.component';
import { PlaceService } from '../../../Services/place.service';
import { AuthService } from '../../../Services/auth.service';
import Place from '../../../Models/place.Model';

@Component({
  selector: 'app-places',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NgIf, NgFor, FormsModule, NavbarComponent, HeaderComponent],
  templateUrl: './place.component.html',
  styleUrls: ['./place.component.css']
})
export class PlacesComponent implements OnInit {
  places: Place[] = [];
  filteredPlaces: Place[] = [];
  searchText: string = '';

  isManager = false;
  form: FormGroup;
  editingId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private placeService: PlaceService,
    private authService: AuthService
  ) {
    this.form = this.fb.group({
      country: ['', [Validators.required, Validators.minLength(2)]]
    });
  }

  ngOnInit(): void {
    this.loadPlaces();
    this.authService.currentUser$.subscribe(user => {
      this.isManager = user?.role === 'manager';
    });
  }

  loadPlaces() {
    this.placeService.getAll().subscribe(p => {
      this.places = p;
      this.filteredPlaces = [...this.places];
    });
  }

  onSearchChange() {
    const lower = this.searchText.toLowerCase();
    this.filteredPlaces = this.places.filter(place =>
      place.country.toLowerCase().includes(lower)
    );
  }

  startEdit(place: Place) {
    this.editingId = place.countryid;
    this.form.patchValue({ country: place.country });
  }

  cancelEdit() {
    this.editingId = null;
    this.form.reset();
  }

  save() {
    if (this.form.invalid) return;
    const value = this.form.value;

    if (this.editingId) {
      this.placeService.update(this.editingId, value).subscribe(() => {
        this.cancelEdit();
        this.loadPlaces();
      });
    } else {
      this.placeService.add(value).subscribe(() => {
        this.cancelEdit();
        this.loadPlaces();
      });
    }
  }

  delete(id: number) {
    if (confirm('Are you sure?')) {
      this.placeService.delete(id).subscribe(() => this.loadPlaces());
    }
  }

  get country() {
    return this.form.get('country');
  }
}

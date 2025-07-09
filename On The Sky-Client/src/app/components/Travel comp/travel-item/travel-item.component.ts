import { Component, Input, Output, EventEmitter } from '@angular/core';
import Travel from '../../../Models/travel.Model';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-travel-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './travel-item.component.html',
  styleUrls: ['./travel-item.component.css'],
})
export class TravelItemComponent {
  @Input() travel!: Travel;
  @Output() approve = new EventEmitter<Travel>();
  @Output() reject = new EventEmitter<Travel>();
}


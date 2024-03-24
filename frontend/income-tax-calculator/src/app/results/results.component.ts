
import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { TaxCalculationResult } from '../models/tax-calculation-result.model';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
  ],
  styleUrls: ['./results.component.scss']
})
export class ResultsComponent {
  @Input() calculationResult: TaxCalculationResult | null = null;
}


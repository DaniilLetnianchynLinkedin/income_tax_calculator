import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { SalaryEntryComponent } from './salary-entry/salary-entry.component';
import { ResultsComponent } from './results/results.component';
import { TaxCalculatorService } from './services/tax-calculator.service';
import { TaxCalculationResult } from './models/tax-calculation-result.model';
import { HttpClientModule } from '@angular/common/http'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: true,
  imports: [ 
    CommonModule,
    HttpClientModule,
    SalaryEntryComponent,
    ResultsComponent,
  ]
})
export class AppComponent {
  calculationResult: TaxCalculationResult | null = null;

  constructor(private taxCalculatorService: TaxCalculatorService) {
  }

  onGrossSalaryEntered(grossSalary: number): void {
    this.taxCalculatorService.calculateTax(grossSalary).subscribe({
      next: (result) => {
        this.calculationResult = result;
      },
      error: (error) => console.error('Error fetching tax calculation', error)
    });
  }
}
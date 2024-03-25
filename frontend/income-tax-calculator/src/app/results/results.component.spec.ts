import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ResultsComponent } from './results.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule } from '@angular/material/card';
import { By } from '@angular/platform-browser';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { TaxCalculatorService } from '../services/tax-calculator.service';

describe('ResultsComponent', () => {
  let component: ResultsComponent;
  let fixture: ComponentFixture<ResultsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NoopAnimationsModule,
        MatCardModule,
        ResultsComponent,
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  
  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display the correct calculation results', () => {
    component.calculationResult = {
      grossAnnualSalary: 40000,
      grossMonthlySalary: 3333.33,
      netAnnualSalary: 29000,
      netMonthlySalary: 2416.67,
      annualTaxPaid: 11000,
      monthlyTaxPaid: 916.67,
    };
  
    fixture.detectChanges(); 
  
    const paragraphs = fixture.debugElement.queryAll(By.css('mat-card-content p'));
    const netAnnualSalaryText = paragraphs.map(p => p.nativeElement.textContent)
                                           .find(text => text.includes('Net Annual Salary'));
  
    expect(netAnnualSalaryText).toContain('29,000.00');
  });
});
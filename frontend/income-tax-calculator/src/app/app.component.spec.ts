import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { TaxCalculatorService } from './services/tax-calculator.service';
import { of } from 'rxjs';
import { TaxCalculationResult } from './models/tax-calculation-result.model';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ResultsComponent } from './results/results.component';
import { SalaryEntryComponent } from './salary-entry/salary-entry.component';

describe('AppComponent', () => {
    let component: AppComponent;
    let fixture: ComponentFixture<AppComponent>;
    let taxCalculatorServiceSpy: jasmine.SpyObj<TaxCalculatorService>;

    beforeEach(async () => {
        const spy = jasmine.createSpyObj('TaxCalculatorService', ['calculateTax']);
        await TestBed.configureTestingModule({
        imports: [
            HttpClientTestingModule,
            NoopAnimationsModule,
            AppComponent,
            ResultsComponent,
            SalaryEntryComponent,
        ],
        providers: [{ provide: TaxCalculatorService, useValue: spy }]
        }).compileComponents();
    
        fixture = TestBed.createComponent(AppComponent);
        component = fixture.componentInstance;
        taxCalculatorServiceSpy = TestBed.inject(TaxCalculatorService) as jasmine.SpyObj<TaxCalculatorService>;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should call calculateTax on grossSalaryEntered and update calculationResult', () => {
        const mockResult: TaxCalculationResult = {
            grossAnnualSalary: 100000,
            grossMonthlySalary: 8333.33,
            netAnnualSalary: 75000,
            netMonthlySalary: 6250,
            annualTaxPaid: 25000,
            monthlyTaxPaid: 2083.33,
        };
        taxCalculatorServiceSpy.calculateTax.and.returnValue(of(mockResult));

        component.onGrossSalaryEntered(100000);
        expect(taxCalculatorServiceSpy.calculateTax.calls.any()).toBeTrue();
        expect(component.calculationResult).toEqual(mockResult);
    });
});

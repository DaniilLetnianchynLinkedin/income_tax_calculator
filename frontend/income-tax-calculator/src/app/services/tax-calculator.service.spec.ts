import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { TaxCalculatorService } from './tax-calculator.service';

describe('TaxCalculatorService', () => {
    let service: TaxCalculatorService;
    let httpTestingController: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
        imports: [HttpClientTestingModule],
        providers: [TaxCalculatorService],
        });
        service = TestBed.inject(TaxCalculatorService);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('#calculateTax should return expected tax calculation result', () => {
        const mockResponse = {
            grossAnnualSalary: 40000,
            grossMonthlySalary: 3333.33,
            netAnnualSalary: 29000,
            netMonthlySalary: 2416.67,
            annualTaxPaid: 11000,
            monthlyTaxPaid: 916.67,
        };

        service.calculateTax(40000).subscribe(result => {
        expect(result).toEqual(mockResponse);
        });

        const req = httpTestingController.expectOne('https://localhost:53208/api/TaxCalculation');
        expect(req.request.method).toEqual('POST');
        req.flush(mockResponse);
    });

    afterEach(() => {
        httpTestingController.verify();
    });
});
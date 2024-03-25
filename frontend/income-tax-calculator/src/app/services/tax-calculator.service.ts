import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TaxCalculationResult } from '../models/tax-calculation-result.model';

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {

  constructor(private http: HttpClient) { }

  calculateTax(grossSalary: number): Observable<TaxCalculationResult> {
    const url = 'https://localhost:53208/api/TaxCalculation';
    return this.http.post<TaxCalculationResult>(url, { grossSalary });
  }
}
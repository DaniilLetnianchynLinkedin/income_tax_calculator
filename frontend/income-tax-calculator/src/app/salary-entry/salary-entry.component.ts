import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardContent } from '@angular/material/card';
import { MatCardTitle } from '@angular/material/card';
import { MatCardHeader } from '@angular/material/card';
import { MatCard } from '@angular/material/card';

@Component({
  selector: 'app-salary-entry',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCardContent,
    MatCardTitle,
    MatCardHeader,
    MatCard,
  ],
  templateUrl: './salary-entry.component.html',
  styleUrls: ['./salary-entry.component.scss']
})
export class SalaryEntryComponent {
  grossSalary: number | null = null;

  @Output() grossSalaryEntered = new EventEmitter<number>();

    submit() {
      if (this.grossSalary !== null) {
        this.grossSalaryEntered.emit(this.grossSalary);
      }
    }
}
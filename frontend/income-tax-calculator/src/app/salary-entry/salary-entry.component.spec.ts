import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { SalaryEntryComponent } from './salary-entry.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('SalaryEntryComponent', () => {
    let component: SalaryEntryComponent;
    let fixture: ComponentFixture<SalaryEntryComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
            FormsModule,
            SalaryEntryComponent,
            NoopAnimationsModule,
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(SalaryEntryComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should emit grossSalaryEntered event on submit', () => {
        spyOn(component.grossSalaryEntered, 'emit');
        component.grossSalary = 50000; // Set the grossSalary input value.
        component.submit(); // Trigger the submit function.

        expect(component.grossSalaryEntered.emit).toHaveBeenCalledWith(50000);
    });
});
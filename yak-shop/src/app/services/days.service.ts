import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DaysService {
  private daysSubject = new BehaviorSubject<number>(0);
  days$ = this.daysSubject.asObservable();

  private previousSubject = new BehaviorSubject<number>(0);
  previous$ = this.previousSubject.asObservable();

  get currentDay(): number {
    return this.daysSubject.value;
  }

  get daysPast(): number {
    return this.daysSubject.value - this.previousSubject.value;
  }

  incrementDays(): void {
    this.previousSubject.next(this.daysSubject.value);
    this.daysSubject.next(this.daysSubject.value + 1);
  }

  incrementFiveDays(): void {
    this.previousSubject.next(this.daysSubject.value);
    this.daysSubject.next(this.daysSubject.value + 5);
  }
}

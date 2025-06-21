import { Component, OnInit } from '@angular/core';
import { DaysService } from '../services/days.service';

@Component({
  selector: 'app-day',
  templateUrl: './day.component.html',
  styleUrls: ['./day.component.css'],
})
export class DayComponent implements OnInit {
  day: number = 0;

  constructor(private daysService: DaysService) {}

  ngOnInit(): void {
    this.daysService.days$.subscribe((value) => {
      this.day = value;
    });
  }

  addDay() {
    this.daysService.incrementDays();
  }

  addDays() {
    this.daysService.incrementFiveDays();
  }
}

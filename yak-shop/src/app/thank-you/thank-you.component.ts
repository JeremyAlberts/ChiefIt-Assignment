import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-thank-you',
  templateUrl: './thank-you.component.html',
  styleUrls: ['./thank-you.component.css'],
})
export class ThankYouComponent {
  orderResult: any;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.orderResult = nav?.extras?.state?.['orderResult'];
    console.log(this.orderResult);
  }
}

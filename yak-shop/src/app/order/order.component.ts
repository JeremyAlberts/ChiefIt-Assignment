import { Component, OnInit } from '@angular/core';
import { YakshopService } from '../services/yakshop.service';
import { Observable } from 'rxjs';
import { IStock } from '../models/stock.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderCommand } from '../models/order.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent implements OnInit {
  stock$: Observable<IStock> = this.yakshopService.stock$;
  orderForm!: FormGroup;

  constructor(
    private yakshopService: YakshopService,
    private fb: FormBuilder,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      name: ['', Validators.required],
      milk: [0, [Validators.required, Validators.min(0)]],
      skins: [
        0,
        [Validators.required, Validators.min(0), Validators.pattern(/^\d+$/)],
      ],
    });
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const formValue = this.orderForm.value;

      const command: OrderCommand = {
        customer: formValue.name,
        order: {
          milk: formValue.milk,
          skins: formValue.skins,
        },
      };

      this.yakshopService.submitOrder(command).subscribe({
        next: (res) => {
          this.yakshopService.refreshStock();
          this.router.navigate(['/thank-you'], {
            state: { orderResult: res },
          });
        },
        error: () => {
          this.router.navigate(['/thank-you']);
        },
      });
    }
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, merge, Observable, Subject, switchMap } from 'rxjs';
import { IYak } from '../models/yak.model';
import { IStock } from '../models/stock.model';
import { DaysService } from './days.service';

@Injectable({
  providedIn: 'root',
})
export class YakshopService {
  private base = 'https://localhost:7126';
  private refreshTrigger$ = new Subject<void>();

  stock$: Observable<IStock>;
  herd$: Observable<IYak[]>;

  constructor(private http: HttpClient, private daysService: DaysService) {
    const dayChanges$ = this.daysService.days$;

    const stockRefresh$ = merge(
      dayChanges$,
      this.refreshTrigger$.pipe(map(() => this.daysService.daysPast))
    );

    this.stock$ = stockRefresh$.pipe(
      switchMap((day) =>
        this.http.get<IStock>(
          `${this.base}/yak-shop/stock/${this.daysService.daysPast}`
        )
      )
    );

    this.herd$ = dayChanges$.pipe(
      switchMap(() =>
        this.http.get<IYak[]>(
          `${this.base}/yak-shop/herd/${this.daysService.daysPast}`
        )
      )
    );
  }

  submitOrder(data: any): Observable<any> {
    return this.http.post(
      `${this.base}/yak-shop/order/${this.daysService.daysPast}`,
      data
    );
  }

  refreshStock(): void {
    this.refreshTrigger$.next();
  }
}

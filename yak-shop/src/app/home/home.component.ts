import { Component, OnInit } from '@angular/core';
import { YakshopService } from '../services/yakshop.service';
import { Observable } from 'rxjs';
import { IYak } from '../models/yak.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  herd$: Observable<IYak[]> = this.yakshopService.herd$;

  constructor(private yakshopService: YakshopService) {}

  ngOnInit(): void {}
}

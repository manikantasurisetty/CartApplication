import { Component, OnInit } from '@angular/core';
import { CarouselConfig } from 'ngx-bootstrap/carousel';
import {carouselList} from './carousellist';
@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.scss'],
  providers: [
    { provide: CarouselConfig, useValue: { interval: 1500, noPause: true, showIndicators: true } }
  ]
})
export class CarouselComponent implements OnInit {
public carouselList:any;
  constructor() { }

  ngOnInit() {
    this.carouselList= carouselList;
  }

}

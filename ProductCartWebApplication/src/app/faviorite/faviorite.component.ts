import { Component, OnInit ,Input, Output,EventEmitter} from '@angular/core';


@Component({
  selector: 'app-faviorite',
  templateUrl: './faviorite.component.html',
  styleUrls: ['./faviorite.component.scss']
})
export class FavioriteComponent implements OnInit {
  @Input() cartList:any;
  @Input() favouriteList:any;
  @Output() navigateToRoute = new EventEmitter<any>();
  constructor() { }

  ngOnInit() {

   
  }
  addToCart(route){
    this.navigateToRoute.emit(route)
   
  }
}

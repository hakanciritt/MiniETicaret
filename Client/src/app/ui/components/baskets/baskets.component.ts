import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { List_Basket_Item } from 'src/app/contracts/basket/list_Basket_Item';
import { Update_Basket_Item } from 'src/app/contracts/basket/update_basket_item';
import { BasketService } from 'src/app/services/common/models/basket.service';

declare var $: any;
@Component({
  selector: 'app-baskets',
  templateUrl: './baskets.component.html',
  styleUrls: []
})
export class BasketsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private basketService: BasketService) {
    super(spinner)
  }
  basketItems: List_Basket_Item[] = [];

  async ngOnInit(): Promise<any> {

    this.showSpinner(SpinnerType.BallAtom);
    this.basketItems = await this.basketService.get()
    this.hideSpinner(SpinnerType.BallAtom);

  }
  async changeQuantity(event: any) {

    const basketItemId = event.target.attributes["id"]?.value;
    const quantity = event.target.value;
    var model = new Update_Basket_Item();
    model.basketItemId = basketItemId;
    model.quantity = quantity;

    await this.basketService.put(model);
    this.hideSpinner(SpinnerType.BallAtom);
  }

  async removeBasketItem(itemId: string) {

    this.showSpinner(SpinnerType.BallAtom);
    await this.basketService.remove(itemId);
    $("." + itemId).fadeOut(2000, () => this.hideSpinner(SpinnerType.BallAtom));
  }
}

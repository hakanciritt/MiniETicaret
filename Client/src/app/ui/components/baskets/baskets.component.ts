import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { List_Basket_Item } from 'src/app/contracts/basket/list_Basket_Item';
import { Update_Basket_Item } from 'src/app/contracts/basket/update_basket_item';
import { Create_Order } from 'src/app/contracts/order/create_order';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { OrderService } from 'src/app/services/common/models/order.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

declare var $: any;
@Component({
  selector: 'app-baskets',
  templateUrl: './baskets.component.html',
  styleUrls: []
})
export class BasketsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService,
    private basketService: BasketService,
    private orderService: OrderService,
    private toastrService: CustomToastrService,
    private router: Router) {
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

  async shoppingComplete() {
    this.showSpinner(SpinnerType.BallAtom);
    const order = new Create_Order();
    order.address = "istanbul / arnavutköy";
    order.description = "siparişmi lütfen güzel hazırlayınız.";
    await this.orderService.create(order);

    this.hideSpinner(SpinnerType.BallAtom);
    this.toastrService.message("Sipariş alınmıştır.", "Sipariş oluşturuldu.", {
      messageType: ToastrMessageType.Info, position: ToastrPosition.TopRight
    });
    this.router.navigate(['/'])
  }
}

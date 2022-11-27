import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Create_Basket_Item } from 'src/app/contracts/basket/create_basket_item';
import { List_Product } from 'src/app/contracts/list_product';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { ProductService } from 'src/app/services/common/models/product.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  products: List_Product[] = [];
  currentPageNo: number;
  totalProductCount: number;
  totalPageCount: number;
  pageSize: number = 12;
  pageList: number[] = [];

  constructor(private productService: ProductService,
    private activatedRoute: ActivatedRoute,
    private basketService: BasketService,
    spinner: NgxSpinnerService,
    private customToastrService : CustomToastrService) {
    super(spinner);
  }

  async ngOnInit() {
    this.activatedRoute.params.subscribe(async params => {
      this.currentPageNo = parseInt(params["pageNo"] ?? 1);

      const data = await this.productService.read(this.currentPageNo - 1, 12, () => { }, error => { });
      this.products = data.products;
      this.totalProductCount = data.totalProductCount;
      this.totalPageCount = Math.ceil(this.totalProductCount / this.pageSize);

      this.pageList = [];

      if (this.currentPageNo - 3 <= 0) {
        for (let index = 1; index < 7; index++) {
          this.pageList.push(index);
        }
      } else if (this.currentPageNo + 3 >= this.totalPageCount) {
        for (let index = this.totalPageCount - 6; index <= this.totalPageCount; index++) {
          this.pageList.push(index);
        }
      } else {
        for (let index = this.totalPageCount - 3; index <= this.totalPageCount + 3; index++) {
          this.pageList.push(index);
        }
      }


    })
  }

  addToBasket(product) {
    const item = new Create_Basket_Item();
    item.productId = product.id;
    item.quantity = 1;

    this.basketService.add(item);
    this.hideSpinner(SpinnerType.BallAtom);
    this.customToastrService.message("Ürün sepete eklenmiştir.","Sepete Eklendi",{
      messageType : ToastrMessageType.Success,
      position : ToastrPosition.TopRight
    });

  }
}

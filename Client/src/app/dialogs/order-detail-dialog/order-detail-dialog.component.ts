import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SingleOrder } from 'src/app/contracts/order/single_order';
import { OrderService } from 'src/app/services/common/models/order.service';
import { BaseDialog } from '../base/base-dialog';

@Component({
  selector: 'app-order-detail-dialog',
  templateUrl: './order-detail-dialog.component.html',
  styleUrls: ['./order-detail-dialog.component.scss']
})
export class OrderDetailDialogComponent extends BaseDialog<OrderDetailDialogComponent> implements OnInit {
  displayedColumns: string[] = ['name', 'price', 'quantity','totalPrice'];
  dataSource: any[] = [];
  clickedRows = new Set<any>();
  order: SingleOrder = null;

  constructor(dialogRef: MatDialogRef<OrderDetailDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: OrderDetailDialogState | string,
    private orderService: OrderService) {
    super(dialogRef);
  }

  async ngOnInit() {
    this.order = await this.orderService.getOrderById(this.data as string);
    this.dataSource = this.order.orderItems;
  }
}

export enum OrderDetailDialogState {
  Close, OrderComplete
}



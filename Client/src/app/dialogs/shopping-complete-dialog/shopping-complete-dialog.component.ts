import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';
declare var $: any;

@Component({
  selector: 'app-shopping-complete-dialog',
  templateUrl: './shopping-complete-dialog.component.html',
  styleUrls: ['./shopping-complete-dialog.component.scss']
})
export class ShoppingCompleteDialogComponent extends BaseDialog<ShoppingCompleteDialogComponent> implements OnDestroy {

  show: boolean = false;

  constructor(dialogRef: MatDialogRef<ShoppingCompleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ShoppingCompleteState) {
    super(dialogRef);
  }

  ngOnDestroy(): void {
    if (!this.show) $("#basketModal").modal("show");
  }

  complete() {
    this.show = true;
  }
}

export enum ShoppingCompleteState {
  Yes,
  No
}

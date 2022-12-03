import { Injectable, ViewContainerRef } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DynamicloadcomponentService {

  constructor() { }

  async loadComponent(component: Components, viewContainerRef: ViewContainerRef) {
    let _component: any = null;

    switch (component) {
      case Components.BasketsComponent:
        _component = (await import("../../ui/components/baskets/baskets.component")).BasketsComponent;
        break;
    }
    viewContainerRef.clear();
    return viewContainerRef.createComponent(_component);
  }

}

export enum Components {
  BasketsComponent
}

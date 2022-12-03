import { Component, ComponentFactoryResolver, Injectable, ViewContainerRef } from '@angular/core';
import { BaseComponent } from 'src/app/base/base.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicloadcomponentService {

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  async loadComponent(component : Components , viewContainerRef : ViewContainerRef) {
    let _component : any = null;

    switch (component) {
      case Components.BasketsComponent:
       _component = (await import("../../ui/components/baskets/baskets.component")).BasketsComponent;
      break;
    }
    viewContainerRef.clear();
    return viewContainerRef.createComponent(this.componentFactoryResolver.resolveComponentFactory(_component));
  }

}

export enum Components {
  BasketsComponent
}

import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { ICartItem } from '../../models/ICart';
import { IOrderItem } from '../../models/IOrderToCreate';

@Component({
    selector: 'app-cart-summary',
    templateUrl: './cart-summary.component.html',
    styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent implements OnInit {
    constructor() {}

    @Output() decrement: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
    @Output() increment: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
    @Output() remove: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();

    @Input() items: ICartItem[] | IOrderItem[] = [];
    @Input() isCart = true;
    @Input() isOrder = false;

    ngOnInit() {}

    public decrementItemQuantity(item: ICartItem): void {
        this.decrement.emit(item);
    }

    public incrementItemQuantity(item: ICartItem): void {
        this.increment.emit(item);
    }

    public removeCartItem(item: ICartItem): void {
        this.remove.emit(item);
    }
}

import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { CartService } from 'src/app/modules/cart/cart.service';
import { Observable } from 'rxjs';
import { ICart, ICartItem } from '../../models/ICart';

@Component({
    selector: 'app-cart-summary',
    templateUrl: './cart-summary.component.html',
    styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent implements OnInit {
    constructor(private cartService: CartService) {}

    @Output() decrement: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
    @Output() increment: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
    @Output() remove: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();

    @Input() isCart = true;

    cart$: Observable<ICart>;

    ngOnInit() {
        this.cart$ = this.cartService.cart$;
    }

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

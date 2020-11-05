import { Component } from '@angular/core';

@Component({
    selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.scss']
})
/** ProductList component*/
export class ProductListComponent {
    /** ProductList ctor */
    public products = [
        {
            title: "first product",
            price: 19.99
        },
        {
            title: "second product",
            price: 17.99
        }
    ]
    constructor() {

    }
}
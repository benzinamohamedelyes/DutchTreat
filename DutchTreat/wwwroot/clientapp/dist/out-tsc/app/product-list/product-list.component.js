import { __decorate } from "tslib";
import { Component } from '@angular/core';
let ProductListComponent = 
/** ProductList component*/
class ProductListComponent {
    constructor() {
        /** ProductList ctor */
        this.products = [
            {
                title: "first product",
                price: 19.99
            },
            {
                title: "second product",
                price: 17.99
            }
        ];
    }
};
ProductListComponent = __decorate([
    Component({
        selector: 'app-product-list',
        templateUrl: './product-list.component.html',
        styleUrls: ['./product-list.component.scss']
    })
    /** ProductList component*/
], ProductListComponent);
export { ProductListComponent };
//# sourceMappingURL=product-list.component.js.map
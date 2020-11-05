/// <reference path="../../../node_modules/@types/jasmine/index.d.ts" />
import { TestBed, async, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { BrowserModule } from "@angular/platform-browser";
import { ProductListComponent } from './product-list.component';
let component;
let fixture;
describe('ProductList component', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [ProductListComponent],
            imports: [BrowserModule],
            providers: [
                { provide: ComponentFixtureAutoDetect, useValue: true }
            ]
        });
        fixture = TestBed.createComponent(ProductListComponent);
        component = fixture.componentInstance;
    }));
    it('should do something', async(() => {
        expect(true).toEqual(true);
    }));
});
//# sourceMappingURL=product-list.component.spec.js.map
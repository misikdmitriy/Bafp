import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CategoryPricingsComponent } from './category-pricings.component';

describe('CategoryPricingsComponent', () => {
  let component: CategoryPricingsComponent;
  let fixture: ComponentFixture<CategoryPricingsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CategoryPricingsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryPricingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

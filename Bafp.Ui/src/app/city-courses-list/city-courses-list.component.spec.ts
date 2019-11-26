import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CityCoursesListComponent } from './city-courses-list.component';

describe('CityCoursesListComponent', () => {
  let component: CityCoursesListComponent;
  let fixture: ComponentFixture<CityCoursesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CityCoursesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CityCoursesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

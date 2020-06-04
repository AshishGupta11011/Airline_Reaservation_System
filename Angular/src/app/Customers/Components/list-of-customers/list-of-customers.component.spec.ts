import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListOfCustomersComponent } from './list-of-customers.component';

describe('ListOfCustomersComponent', () => {
  let component: ListOfCustomersComponent;
  let fixture: ComponentFixture<ListOfCustomersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListOfCustomersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListOfCustomersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

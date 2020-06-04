import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminCancellationComponent } from './admin-cancellation.component';

describe('AdminCancellationComponent', () => {
  let component: AdminCancellationComponent;
  let fixture: ComponentFixture<AdminCancellationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdminCancellationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminCancellationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

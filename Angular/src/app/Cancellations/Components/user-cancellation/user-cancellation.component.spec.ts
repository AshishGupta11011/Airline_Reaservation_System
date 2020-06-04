import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserCancellationComponent } from './user-cancellation.component';

describe('UserCancellationComponent', () => {
  let component: UserCancellationComponent;
  let fixture: ComponentFixture<UserCancellationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserCancellationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserCancellationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

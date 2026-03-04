import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { UnauthenticatedLayoutComponent } from './unauthenticated-layout.component';

describe('UnauthenticatedLayoutComponent', () => {
  let component: UnauthenticatedLayoutComponent;
  let fixture: ComponentFixture<UnauthenticatedLayoutComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ UnauthenticatedLayoutComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnauthenticatedLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestApiCallComponent } from './test-api-call.component';

describe('TestApiCallComponent', () => {
  let component: TestApiCallComponent;
  let fixture: ComponentFixture<TestApiCallComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestApiCallComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TestApiCallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

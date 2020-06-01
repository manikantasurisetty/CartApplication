import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FiltetemplateComponent } from './filtetemplate.component';

describe('FiltetemplateComponent', () => {
  let component: FiltetemplateComponent;
  let fixture: ComponentFixture<FiltetemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FiltetemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FiltetemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

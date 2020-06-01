import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FavioriteComponent } from './faviorite.component';

describe('FavioriteComponent', () => {
  let component: FavioriteComponent;
  let fixture: ComponentFixture<FavioriteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FavioriteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FavioriteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

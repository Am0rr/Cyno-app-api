import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LitterList } from './litter-list';

describe('LitterList', () => {
  let component: LitterList;
  let fixture: ComponentFixture<LitterList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LitterList],
    }).compileComponents();

    fixture = TestBed.createComponent(LitterList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

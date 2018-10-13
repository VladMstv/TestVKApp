/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { VkReportComponent } from './vk-report.component';

describe('VkReportComponent', () => {
  let component: VkReportComponent;
  let fixture: ComponentFixture<VkReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VkReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VkReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

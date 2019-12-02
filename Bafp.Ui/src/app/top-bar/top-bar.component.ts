import { Component, OnInit } from '@angular/core';
import { Constants } from '../constants';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.css']
})
export class TopBarComponent implements OnInit {
  averageStudents: number = Constants.averageStudents;

  constructor() { }

  ngOnInit() {
  }

  save() {
    Constants.averageStudents = this.averageStudents;
  }
}
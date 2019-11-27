import { Component, OnInit, Injectable } from '@angular/core';
import { CityResponse } from '../models/cityResponse';
import { City, CityDto } from '../models/city';
import { HttpService } from '../http.service';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  cities: City[];
  newCity: CityDto = {
    name: "",
    categoryName: ""
  };
  addMode = false;

  constructor(private httpService: HttpService) {
  }

  ngOnInit() {
    this.httpService.getCities().then((data: CityResponse) => {
      this.cities = data.cities;
    });
  }

  addNew() {
    this.httpService.addNewCity(this.newCity)
      .then(() => window.location.reload());
  }
}

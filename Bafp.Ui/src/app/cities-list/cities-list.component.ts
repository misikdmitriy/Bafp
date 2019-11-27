import { Component, OnInit, Injectable } from '@angular/core';
import { CitiesResponse } from '../models/cityResponse';
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
    categoryId: 0
  };
  addMode = false;

  constructor(private httpService: HttpService) {
  }

  ngOnInit() {
    this.httpService.getCities().then((data: CitiesResponse) => {
      this.cities = data.cities;
    });
  }

  addNew() {
    this.httpService.addNewCity(this.newCity)
      .then(() => window.location.reload());
  }
}

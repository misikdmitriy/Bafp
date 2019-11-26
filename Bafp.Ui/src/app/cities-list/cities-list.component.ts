import { Component, OnInit, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { CityResponse } from '../models/cityResponse';
import { City } from '../models/city';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  citiesUrl = 'api/cities';
  cities: City[];
  newCity: City = {
    id: 0,
    name: "",
    categoryName: ""
  };
  addMode = false;

  constructor(private http: HttpClient) {
  }

  ngOnInit() {
    this.getCities().subscribe((data: CityResponse) => {
      this.cities = data.cities;
    });
  }

  getCities() {
    return this.http.get(environment.apiUrl + this.citiesUrl);
  }

  addNew() {
    this.http.put(environment.apiUrl + this.citiesUrl, { city: this.newCity })
      .subscribe(() => window.location.reload());
  }
}

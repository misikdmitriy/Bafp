import { Component, OnInit, Injectable } from '@angular/core';
import { CitiesResponse } from '../models/cityResponse';
import { City, CityDto } from '../models/city';
import { HttpService } from '../http.service';
import { PricingCategory } from '../models/pricingCategory';
import { PricingCategoriesResponse } from '../models/pricingCategoriesResponse';
import { CityViewModel } from '../models/cityViewModel';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  cities: CityViewModel[];
  newCity: CityDto = {
    name: "",
    categoryId: 0
  };
  addMode = false;
  pricingCategories: PricingCategory[];

  constructor(private httpService: HttpService) {
  }

  ngOnInit() {
    Promise.all([this.httpService.getCities(), this.httpService.getPricingCategories()])
      .then((response: [CitiesResponse, PricingCategoriesResponse]) => {
        let cities: City[] = response[0].cities;
        let pricingCategories: PricingCategory[] = response[1].categories;

        this.pricingCategories = pricingCategories;
        this.newCity.categoryId = this.pricingCategories[0].id;

        this.cities = cities.map((city: City) => {
          let category: PricingCategory = pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

          let viewModel: CityViewModel = new CityViewModel();
          viewModel.cityId = city.id;
          viewModel.cityName = city.name;
          viewModel.categoryName = category.name;
          viewModel.categoryId = category.id;

          return viewModel;
        });
      });
  }

  addNew() {
    this.httpService.addNewCity(this.newCity)
      .then(() => window.location.reload());
  }
}

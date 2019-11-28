import { Component, OnInit, Injectable } from '@angular/core';
import { CitiesResponse } from '../models/responses/cityResponse';
import { City } from '../models/contracts/city';
import { HttpService } from '../http.service';
import { PricingCategory } from '../models/contracts/pricingCategory';
import { PricingCategoriesResponse } from '../models/responses/pricingCategoriesResponse';
import { CityViewModel } from '../models/view-models/cityViewModel';
import { NewCityResponse } from '../models/responses/newCityResponse';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  cities: CityViewModel[];
  newCity: City = {
    id: 0,
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
          viewModel.editMode = false;

          return viewModel;
        });
      });
  }

  edit(cityView: CityViewModel) {
    let city: City = new City();

    city.id = cityView.cityId;
    city.name = cityView.cityName;
    city.categoryId = +cityView.categoryId;

    this.httpService.addNewCity(city)
      .then(() => {
        let category: PricingCategory = this.pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

        cityView.categoryName = category.name;
        cityView.editMode = false
      });
  }

  addNew() {
    this.addMode = false;

    if (this.cities.findIndex((city: CityViewModel) => city.cityName.toLowerCase() === this.newCity.name.toLowerCase()) !== -1) {
      return;
    }

    this.httpService.addNewCity(this.newCity)
      .then((cityResponse: NewCityResponse) => {
        let city: City = cityResponse.city;
        let category: PricingCategory = this.pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

        let viewModel: CityViewModel = new CityViewModel();
        viewModel.cityId = city.id;
        viewModel.cityName = city.name;
        viewModel.categoryName = category.name;
        viewModel.categoryId = category.id;

        this.cities.push(viewModel);
      });
  }
}

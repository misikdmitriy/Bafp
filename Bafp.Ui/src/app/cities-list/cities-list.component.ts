import { Component, OnInit, Injectable } from '@angular/core';
import { CitiesResponse } from '../models/responses/cityResponse';
import { City } from '../models/contracts/city';
import { HttpService } from '../http.service';
import { PricingCategory } from '../models/contracts/pricingCategory';
import { PricingCategoriesResponse } from '../models/responses/pricingCategoriesResponse';
import { CityViewModel } from '../models/view-models/cityViewModel';
import { NewCityResponse } from '../models/responses/newCityResponse';
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { FieldType, EditMode, FieldDescriptor } from '../models/service/FieldDescriptor';
import { ChooseOption } from '../models/contracts/option'

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  cities: CityViewModel[];
  pricingCategories: PricingCategory[];
  modelDescriptor: ModelDescriptor;

  constructor(private httpService: HttpService) {
    this.modelDescriptor = {
      canRemove: false,
      canEdit: true,
      canAdd: true,
      fieldsDescriptor: [
        {
          idName: "cityId", keyName: "cityName", name: "City name",
          addMode: EditMode.Text, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/city", "$cityId"] }
        },
        {
          idName: "categoryId", keyName: "categoryName", name: "Category name",
          addMode: EditMode.Dropdown, editMode: EditMode.Dropdown, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/category", "$categoryId"] }
        }
      ],
      editCallback: (cityView: CityViewModel) => {
        let city: City = new City();

        city.id = cityView.cityId;
        city.name = cityView.cityName;
        city.categoryId = +cityView.categoryId;

        this.httpService.addNewCity(city)
          .then(() => {
            let category: PricingCategory = this.pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

            cityView.categoryName = category.name;
            cityView.isEditing = false;
          });
      },
      addCallback: (cityView: CityViewModel) => {
        if (this.cities.findIndex((city: CityViewModel) => city.cityName.toLowerCase() === cityView.cityName.toLowerCase()) !== -1) {
          return;
        }

        let city: City = new City();

        city.id = cityView.cityId;
        city.name = cityView.cityName;
        city.categoryId = +cityView.categoryId;

        this.httpService.addNewCity(city)
          .then((cityResponse: NewCityResponse) => {
            let city: City = cityResponse.city;
            let category: PricingCategory = this.pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

            this.cities.push({
              cityId: city.id,
              cityName: city.name,
              categoryName: category.name,
              categoryId: category.id,
              isEditing: false
            });
          });
      },
      removeCallback: null
    };
  }

  ngOnInit() {
    Promise.all([this.httpService.getCities(), this.httpService.getPricingCategories()])
      .then((response: [CitiesResponse, PricingCategoriesResponse]) => {
        let cities: City[] = response[0].cities;
        let pricingCategories: PricingCategory[] = response[1].categories;

        this.modelDescriptor.fieldsDescriptor[1].possibleValues = pricingCategories.map((pricingCategory: PricingCategory) => {
          let option = new ChooseOption();
          option.id = pricingCategory.id;
          option.name = pricingCategory.name;
          return option;
        })

        this.pricingCategories = pricingCategories;

        this.cities = cities.map((city: City) => {
          let category: PricingCategory = pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);

          let viewModel: CityViewModel = new CityViewModel();
          viewModel.cityId = city.id;
          viewModel.cityName = city.name;
          viewModel.categoryName = category.name;
          viewModel.categoryId = category.id;
          viewModel.isEditing = false;

          return viewModel;
        });
      });
  }
}

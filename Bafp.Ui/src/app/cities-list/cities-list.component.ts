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
import { CitiesTotalResponse } from '../models/responses/citiesTotalResponse';
import { CityTotal } from '../models/contracts/cityTotal';
import { Constants } from '../constants';
import { CityCoursesResponse } from '../models/responses/cityCourseResponse';
import { CityCourse } from '../models/contracts/cityCourse';
import { CoursesResponse } from '../models/responses/coursesResponse';
import { Course } from '../models/contracts/course';

@Component({
  selector: 'app-cities-list',
  templateUrl: './cities-list.component.html',
  styleUrls: ['./cities-list.component.css']
})
export class CitiesListComponent implements OnInit {
  cities: CityViewModel[];
  pricingCategories: PricingCategory[];
  modelDescriptor: ModelDescriptor;
  header: string = "Cities list";

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
          idName: "courses", keyName: "courses", name: "Courses",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.List, args: null
        },
        {
          idName: "categoryId", keyName: "categoryName", name: "Category name",
          addMode: EditMode.Dropdown, editMode: EditMode.Dropdown, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/category", "$categoryId"] }
        },
        {
          idName: "totalStudents", keyName: "totalStudents", name: "Total (12 students)",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
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

            this.cities.push(new CityViewModel({
              cityId: city.id,
              cityName: city.name,
              categoryName: category.name,
              categoryId: category.id,
              isEditing: false,
              total: 0.00,
              courses: []
            }));
          });
      },
      removeCallback: null
    };
  }

  ngOnInit() {
    Promise.all([this.httpService.getCities(), this.httpService.getPricingCategories(), this.httpService.getCitiesTotal(),
    this.httpService.getAllCityCourses(), this.httpService.getCourses()])
      .then((response: [CitiesResponse, PricingCategoriesResponse, CitiesTotalResponse, CityCoursesResponse, CoursesResponse]) => {
        let cities: City[] = response[0].cities;
        let pricingCategories: PricingCategory[] = response[1].categories;
        let totals: CityTotal[] = response[2].total;
        let cityCourses: CityCourse[] = response[3].cityCourses;
        let courses: Course[] = response[4].courses;

        this.modelDescriptor.fieldsDescriptor[2].possibleValues = pricingCategories.map((pricingCategory: PricingCategory) => {
          let option: ChooseOption = {
            id: pricingCategory.id,
            name: pricingCategory.name
          };
          return option;
        });

        this.pricingCategories = pricingCategories;

        this.cities = cities.map((city: City) => {
          let category: PricingCategory = pricingCategories.find((category: PricingCategory) => category.id === city.categoryId);
          let total: CityTotal = totals.find((total: CityTotal) => total.cityId === city.id);
          let cityCourse: CityCourse[] = cityCourses.filter((course: CityCourse) => course.cityId === city.id);

          let viewModel: CityViewModel = new CityViewModel({
            cityId: city.id,
            cityName: city.name,
            categoryName: category.name,
            categoryId: category.id,
            isEditing: false,
            total: total.total,
            courses: cityCourse.map((cityCourse: CityCourse) => {
              let course: Course = courses.find((c: Course) => cityCourse.courseId === c.id);
              return `${cityCourse.count} ${course.name}`;
            })
          });

          return viewModel;
        });
      });
  }

  total(): number {
    return this.cities && this.cities.reduce((accum: number, value: CityViewModel) => {
      return accum + value.total;
    }, 0.00);
  }

  totalStudents(): number {
    let total = this.total();
    return total && total * Constants.averageStudents;
  }
}

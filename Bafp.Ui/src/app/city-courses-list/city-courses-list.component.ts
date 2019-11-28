import { Component, OnInit } from '@angular/core';
import { CityCoursesResponse } from '../models/responses/cityCourseResponse';
import { ActivatedRoute } from "@angular/router";
import { CityCourse } from '../models/contracts/cityCourse';
import { CoursesResponse } from '../models/responses/coursesResponse';
import { Course } from '../models/contracts/course';
import { HttpService } from '../http.service';
import { CoursePricingResponse } from '../models/responses/coursePricingResponse';
import { CoursePricing } from '../models/contracts/coursePricing';
import { CityCourseViewModel } from '../models/view-models/CityCourseViewModel';
import { CitiesResponse } from '../models/responses/cityResponse';
import { City } from '../models/contracts/city';

@Component({
  selector: 'app-city-courses-list',
  templateUrl: './city-courses-list.component.html',
  styleUrls: ['./city-courses-list.component.css']
})
export class CityCoursesListComponent implements OnInit {
  city: City;
  allCourses: Course[];
  cityCourses: CityCourseViewModel[];
  showAll = true

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      let cityId: number = +params.cityId;

      Promise.all([this.httpService.getCityCourses(cityId), this.httpService.getCities(), this.httpService.getCourses()])
        .then((response: [CityCoursesResponse, CitiesResponse, CoursesResponse]) => {
          let cityCourses: CityCourse[] = response[0].cityCourses;
          let cities: City[] = response[1].cities;
          let allCourses: Course[] = response[2].courses;

          this.city = cities.find((city: City) => city.id === cityId);

          this.httpService.getCoursePricing(this.city.categoryId).then((pricingResponse: CoursePricingResponse) => {
            let prices: CoursePricing[] = pricingResponse.coursePriceList;

            this.cityCourses = cityCourses.map((value: CityCourse) => {
              let price: CoursePricing = prices.find((price: CoursePricing) => price.courseId === value.courseId);
              let course: Course = allCourses.find((course: Course) => course.id === value.courseId);

              let viewModel: CityCourseViewModel = new CityCourseViewModel();
              viewModel.count = value.count;
              viewModel.courseId = course.id;
              viewModel.courseName = course.name;
              viewModel.price = price.price;
              viewModel.editMode = false;

              return viewModel;
            });
          });
        })
    });
  }

  ngOnInit() {
  }

  total(): number {
    return this.cityCourses && this.cityCourses.reduce((accum: number, value: CityCourseViewModel) => {
      return accum + value.total;
    }, 0.00);
  }

  edit(course: CityCourseViewModel): Promise<Object> {
    var dto: CityCourse = {
      cityId: this.city.id,
      count: course.count,
      courseId: course.courseId
    };

    return this.httpService.addNewCityCourse(dto)
      .then(() => course.editMode = false);
  }
}

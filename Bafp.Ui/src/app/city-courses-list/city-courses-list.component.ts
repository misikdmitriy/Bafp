import { Component, OnInit } from '@angular/core';
import { CityCoursesResponse } from '../models/cityCourseResponse';
import { ActivatedRoute } from "@angular/router";
import { CityCourse } from '../models/cityCourse';
import { CoursesResponse } from '../models/coursesResponse';
import { Course } from '../models/course';
import { HttpService } from '../http.service';
import { CoursePricingResponse } from '../models/coursePricingResponse';
import { CoursePricing } from '../models/coursePricing';
import { CourseViewModel } from '../models/courseViewModel';
import { CityCourseDto } from '../models/cityCourseDto';
import { CitiesResponse } from '../models/cityResponse';
import { City } from '../models/city';

@Component({
  selector: 'app-city-courses-list',
  templateUrl: './city-courses-list.component.html',
  styleUrls: ['./city-courses-list.component.css']
})
export class CityCoursesListComponent implements OnInit {
  city: City;
  addMode = false;
  newCityCourse: CityCourseDto = {
    cityId: 0,
    count: 1,
    courseId: 0
  };
  allCourses: Course[];
  availableCourses: Course[];
  cityCourses: CourseViewModel[]

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      let cityId: number = +params.cityId;
      this.newCityCourse.cityId = cityId;

      Promise.all([this.httpService.getCityCourses(cityId), this.httpService.getCities(), this.httpService.getCourses()])
        .then((response: [CityCoursesResponse, CitiesResponse, CoursesResponse]) => {
          let cityCourses: CityCourse[] = response[0].cityCourses;
          let cities: City[] = response[1].cities;
          let allCourses: Course[] = response[2].courses;

          this.availableCourses = allCourses.filter((value: Course) => {
            return cityCourses.findIndex((cityCourse: CityCourse) => {
              return cityCourse.courseId === value.id;
            }) === -1;
          });

          this.city = cities.find((city: City) => city.id === cityId);

          this.httpService.getCoursePricing(this.city.categoryId).then((pricingResponse: CoursePricingResponse) => {
            let prices: CoursePricing[] = pricingResponse.coursePriceList;

            this.cityCourses = cityCourses.map((value: CityCourse) => {
              let price: CoursePricing = prices.find((price: CoursePricing) => price.courseId === value.courseId);
              let course: Course = allCourses.find((course: Course) => course.id === value.courseId);

              let viewModel: CourseViewModel = new CourseViewModel();
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
    return this.cityCourses && this.cityCourses.reduce((accum: number, value: CourseViewModel) => {
      return accum + value.total;
    }, 0.00);
  }

  upsertView(course: CourseViewModel): Promise<void> {
    var dto: CityCourseDto = {
      cityId: this.city.id,
      count: course.count,
      courseId: course.courseId
    };

    return this.upsert(dto, (value: Object) => course.editMode = false);
  }

  upsert(course: CityCourseDto, callback: (value: Object) => void = null): Promise<void> {
    callback = callback || function (value: Object): void { window.location.reload(); };
    return this.httpService.addNewCourse(course).then(callback);
  }
}

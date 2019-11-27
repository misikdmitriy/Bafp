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

@Component({
  selector: 'app-city-courses-list',
  templateUrl: './city-courses-list.component.html',
  styleUrls: ['./city-courses-list.component.css']
})
export class CityCoursesListComponent implements OnInit {
  cityName: string;
  addMode = false;
  newCityCourse: CityCourse = {
    cityName: "",
    count: 1,
    courseName: ""
  };
  allCourses: Course[];
  availableCourses: Course[];
  cityCourses: CourseViewModel[]

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      this.cityName = params.cityName;
      // ToDo: use real category
      Promise.all([this.httpService.getCityCourses(this.cityName), this.httpService.getCourses(), this.httpService.getCoursePricing("X")])
        .then((response: [CityCoursesResponse, CoursesResponse, CoursePricingResponse]) => {
          let cityCourse = response[0].cityCourses;
          this.allCourses = response[1].courses;
          let prices = response[2].coursePriceList;

          this.availableCourses = response[1].courses.filter((value: Course) => {
            return cityCourse.findIndex((cityCourse: CityCourse) => {
              return cityCourse.courseName === value.name;
            }) === -1;
          });

          this.cityCourses = cityCourse.map((value: CityCourse) => {
            let price = prices.find((price: CoursePricing) => price.courseName === value.courseName);
            let viewModel = new CourseViewModel();
            viewModel.count = value.count;
            viewModel.courseName = value.courseName;
            viewModel.price = price.price;
            viewModel.editMode = false;
            return viewModel;
          })
        })
    });
  }

  ngOnInit() {
  }

  total() {
    return this.cityCourses && this.cityCourses.reduce((accum: number, value: CourseViewModel) => {
      return accum + value.total;
    }, 0.00);
  }

  getAvailableCourses(course: CourseViewModel) {
    if (this.availableCourses && this.allCourses) {
      var copy = this.availableCourses.slice();
      var courseToAdd = this.allCourses.find((value: Course) => value.name === course.courseName);
      copy.push(courseToAdd);
      return copy;
    }

    return [];
  }

  addNew() {
    var cityCourse: CityCourse = {
      cityName: this.cityName,
      courseName: this.newCityCourse.courseName,
      count: this.newCityCourse.count
    };

    this.httpService.addNewCourse(cityCourse).then(() => {
      window.location.reload()
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../http.service';
import { CityCoursesResponse } from '../models/responses/cityCourseResponse';
import { CitiesResponse } from '../models/responses/cityResponse';
import { CoursesResponse } from '../models/responses/coursesResponse';
import { CityCourse } from '../models/contracts/cityCourse';
import { City } from '../models/contracts/city';
import { Course } from '../models/contracts/course';
import { CourseViewModel } from '../models/view-models/courseViewModel';
import { CoursePricingResponse } from '../models/responses/coursePricingResponse';
import { CoursePricing } from '../models/contracts/coursePricing';
import { CoursePricingViewModel } from '../models/view-models/coursePricingViewModel';
import { PricingCategoriesResponse } from '../models/responses/pricingCategoriesResponse';
import { PricingCategory } from '../models/contracts/pricingCategory';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {
  course: Course;
  cityCourses: CourseViewModel[];

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      let courseId: number = +params.courseId;

      Promise.all([this.httpService.getCityCoursesByCourse(courseId), this.httpService.getCities(), this.httpService.getCourses(), 
        this.httpService.getCoursePricingByCourse(courseId), this.httpService.getPricingCategories()])
        .then((response: [CityCoursesResponse, CitiesResponse, CoursesResponse, CoursePricingResponse, PricingCategoriesResponse]) => {
          let cityCourses: CityCourse[] = response[0].cityCourses;
          let cities: City[] = response[1].cities;
          let allCourses: Course[] = response[2].courses;
          let priceList: CoursePricing[] = response[3].coursePriceList;
          let categories: PricingCategory[] = response[4].categories;

          this.course = allCourses.find((course: Course) => course.id === courseId);

          this.cityCourses = cityCourses.map((cityCourse: CityCourse) => {
            let city: City = cities.find((city: City) => city.id === cityCourse.cityId);
            let price: CoursePricing = priceList.find((price: CoursePricing) => price.categoryId === city.categoryId);
            let category: PricingCategory = categories.find((category: PricingCategory) => category.id === city.categoryId);

            let viewModel: CourseViewModel = new CourseViewModel();

            viewModel.cityId = cityCourse.cityId;
            viewModel.cityName = city.name;
            viewModel.categoryId = category.id;
            viewModel.categoryName = category.name;
            viewModel.count = cityCourse.count;
            viewModel.price = price.price;

            return viewModel;
          });
        })
    });
  }

  ngOnInit() {
  }

  count() {
    return this.cityCourses && this.cityCourses.reduce((prev: number, curr: CourseViewModel) => prev + curr.count, 0);
  }

  total() {
    return this.cityCourses && this.cityCourses.reduce((prev: number, curr: CourseViewModel) => prev + curr.total, 0.00);
  }
}

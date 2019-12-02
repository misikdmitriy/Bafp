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
import { PricingCategoriesResponse } from '../models/responses/pricingCategoriesResponse';
import { PricingCategory } from '../models/contracts/pricingCategory';
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { EditMode, FieldType } from '../models/service/FieldDescriptor';
import { Constants } from '../constants';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {
  course: Course;
  cityCourses: CourseViewModel[];
  modelDescriptor: ModelDescriptor;
  header: string;

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.modelDescriptor = {
      canRemove: false,
      canEdit: false,
      canAdd: false,
      fieldsDescriptor: [
        {
          idName: "cityId", keyName: "cityName", name: "City Name",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/city", "$cityId"] }
        },
        {
          idName: "categodyId", keyName: "category", name: "Category",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/category", "$categoryId"] }
        },
        {
          idName: "count", keyName: "count", name: "Count",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        },
        {
          idName: "price", keyName: "price", name: "Price",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        },
        {
          idName: "totalStudents", keyName: "totalStudents", name: "Total (12 students)",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        }
      ],
      editCallback: null,
      addCallback: null,
      removeCallback: null
    };
  }

  ngOnInit() {
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
          this.header = `Course ${this.course.name}`;

          this.cityCourses = cityCourses.map((cityCourse: CityCourse) => {
            let city: City = cities.find((city: City) => city.id === cityCourse.cityId);
            let price: CoursePricing = priceList.find((price: CoursePricing) => price.categoryId === city.categoryId);
            let category: PricingCategory = categories.find((category: PricingCategory) => category.id === city.categoryId);

            return new CourseViewModel({
              cityId: cityCourse.cityId,
              cityName: city.name,
              categoryId: category.id,
              categoryName: category.name,
              count: cityCourse.count,
              price: price.price,
            });
          });
        })
    });
  }

  count() {
    return this.cityCourses && this.cityCourses.reduce((prev: number, curr: CourseViewModel) => prev + curr.count, 0);
  }

  total() {
    return this.cityCourses && this.cityCourses.reduce((prev: number, curr: CourseViewModel) => prev + curr.total, 0.00);
  }

  totalStudents(): number {
    let total = this.total();
    return total && total * Constants.averageStudents;
  }
}

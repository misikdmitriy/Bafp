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
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { EditMode, FieldType } from '../models/service/FieldDescriptor';
import { Constants } from '../constants';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {
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
          idName: "totalStudents", keyName: "totalStudents", name: `Total (X students)`,
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

      Promise.all([this.httpService.getCityCoursesByCourse(courseId), this.httpService.getCities(), this.httpService.getCourses([courseId]),
      this.httpService.getCoursePricingByCourse(courseId)])
        .then((response: [CityCoursesResponse, CitiesResponse, CoursesResponse, CoursePricingResponse]) => {
          let cityCourses: CityCourse[] = response[0].cityCourses;
          let cities: City[] = response[1].cities;
          let courses: Course[] = response[2].courses;
          let priceList: CoursePricing[] = response[3].coursePriceList;

          this.header = `Course ${courses[0].name}`;

          this.cityCourses = cityCourses.map((cityCourse: CityCourse) => {
            let city: City = cities.find((city: City) => city.id === cityCourse.cityId);
            let price: CoursePricing = priceList.find((price: CoursePricing) => price.categoryId === city.categoryId);

            return new CourseViewModel({
              cityId: cityCourse.cityId,
              cityName: city.name,
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

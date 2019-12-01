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
import { FieldType, EditMode } from '../models/service/FieldDescriptor';
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { Constants } from '../constants';

@Component({
  selector: 'app-city-courses-list',
  templateUrl: './city-courses-list.component.html',
  styleUrls: ['./city-courses-list.component.css']
})
export class CityCoursesListComponent implements OnInit {
  city: City;
  allCourses: Course[];
  cityCourses: CityCourseViewModel[];
  modelDescriptor: ModelDescriptor;

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.modelDescriptor = {
      canRemove: false,
      canEdit: true,
      canAdd: false,
      fieldsDescriptor: [
        {
          idName: "courseId", keyName: "courseName", name: "Course Name",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/courses", "$courseId"] }
        },
        {
          idName: "count", keyName: "count", name: "Count",
          addMode: EditMode.None, editMode: EditMode.PlusMinus, possibleValues: null,
          type: FieldType.Text, args: null
        },
        {
          idName: "price", keyName: "price", name: "Price",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        },
        {
          idName: "total", keyName: "total", name: "Total",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        },
        {
          idName: "totalStudents", keyName: "totalStudents", name: "Total (12 students)",
          addMode: EditMode.None, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Text, args: null
        }
      ],
      editCallback: (course: CityCourseViewModel) => {
        var dto: CityCourse = {
          cityId: this.city.id,
          count: course.count,
          courseId: course.courseId
        };

        this.httpService.addNewCityCourse(dto);
      },
      addCallback: null,
      removeCallback: null
    };
  }

  ngOnInit() {
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

              return new CityCourseViewModel({
                count: value.count,
                courseId: course.id,
                courseName: course.name,
                price: price.price,
                isEditing: false
              });
            });
          });
        })
    });
  }

  total(): number {
    return this.cityCourses && this.cityCourses.reduce((accum: number, value: CityCourseViewModel) => {
      return accum + value.total;
    }, 0.00);
  }

  totalStudents(): number {
    let total = this.total();
    return total && total * Constants.averageStudents;
  }
}

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
import { FieldType, EditMode, FieldDescriptor } from '../models/service/FieldDescriptor';
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
  prices: CoursePricing[];
  header: string;

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.modelDescriptor = {
      canRemove: false,
      canEdit: true,
      canAdd: true,
      fieldsDescriptor: [
        {
          idName: "courseId", keyName: "courseName", name: "Course Name",
          addMode: EditMode.Dropdown, editMode: EditMode.None, possibleValues: [],
          type: FieldType.Link, args: { routerLink: ["/courses", "$courseId"] }
        },
        {
          idName: "count", keyName: "count", name: "Count",
          addMode: EditMode.PlusMinus, editMode: EditMode.PlusMinus, possibleValues: null,
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
      editCallback: (course: CityCourseViewModel) => {
        var dto: CityCourse = {
          cityId: +this.city.id,
          count: course.count,
          courseId: +course.courseId
        };

        this.httpService.addNewCityCourse(dto).then(() => {
          if (dto.count <= 0) {
            this.cityCourses.splice(this.cityCourses
              .findIndex((cityCourse: CityCourseViewModel) => cityCourse.courseId === dto.courseId), 1);
            this.updateAvailableCourses();
          }
        });
      },
      addCallback: (course: CityCourseViewModel) => {
        var dto: CityCourse = {
          cityId: +this.city.id,
          count: course.count,
          courseId: +course.courseId
        };

        if (dto.count > 0) {
          this.httpService.addNewCityCourse(dto).then(() => {
            if (dto.count > 0) {
              this.cityCourses.push(new CityCourseViewModel({
                count: dto.count,
                courseId: dto.courseId,
                courseName: this.allCourses.find((course: Course) => course.id === dto.courseId).name,
                isEditing: false,
                price: this.prices.find((price: CoursePricing) => price.courseId === dto.courseId).price
              }));

              this.updateAvailableCourses();
            }
          });
        }
      },
      removeCallback: null
    };
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      let cityId: number = +params.cityId;

      Promise.all([this.httpService.getCityCourses(cityId), this.httpService.getCities([cityId]), this.httpService.getCourses()])
        .then((response: [CityCoursesResponse, CitiesResponse, CoursesResponse]) => {
          let cityCourses: CityCourse[] = response[0].cityCourses;
          let cities: City[] = response[1].cities;
          let allCourses: Course[] = response[2].courses;

          this.allCourses = allCourses;
          this.city = cities[0];
          this.header = `City ${this.city.name}`;

          this.httpService.getCoursePricing([this.city.categoryId]).then((pricingResponse: CoursePricingResponse) => {
            let prices: CoursePricing[] = pricingResponse.coursePriceList;
            this.prices = prices;

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

            this.updateAvailableCourses();
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

  updateAvailableCourses(): void {
    let coursesDescriptor = this.modelDescriptor.fieldsDescriptor.find((fd: FieldDescriptor) => fd.idName === "courseId");
    coursesDescriptor.possibleValues = this.allCourses.filter((course: Course) => !this.cityCourses.some((cc: CityCourseViewModel) => cc.courseId === course.id));
  }
}

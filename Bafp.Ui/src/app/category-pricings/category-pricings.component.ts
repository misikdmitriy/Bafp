import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpService } from '../http.service';
import { CoursePricingResponse } from '../models/responses/coursePricingResponse';
import { CoursesResponse } from '../models/responses/coursesResponse';
import { CoursePricing } from '../models/contracts/coursePricing';
import { Course } from '../models/contracts/course';
import { CoursePricingViewModel } from '../models/view-models/coursePricingViewModel';
import { NewCourseResponse } from '../models/responses/newCourseResponse';
import { ModelDescriptor } from '../models/service/modelDescriptor';
import { FieldType, EditMode, FieldDescriptor } from '../models/service/FieldDescriptor';
import { PricingCategoriesResponse } from '../models/responses/pricingCategoriesResponse';
import { PricingCategory } from '../models/contracts/pricingCategory';

@Component({
  selector: 'app-category-pricings',
  templateUrl: './category-pricings.component.html',
  styleUrls: ['./category-pricings.component.css']
})
export class CategoryPricingsComponent implements OnInit {
  coursePrices: CoursePricingViewModel[];
  categoryId: number;
  modelDescriptor: ModelDescriptor;
  header: string;

  constructor(private httpService: HttpService, private route: ActivatedRoute) {
    this.modelDescriptor = {
      canRemove: true,
      canEdit: true,
      canAdd: true,
      fieldsDescriptor: [
        {
          idName: "courseId", keyName: "courseName", name: "Course Name",
          addMode: EditMode.Text, editMode: EditMode.None, possibleValues: null,
          type: FieldType.Link, args: { routerLink: ["/courses", "$courseId"] }
        },
        {
          idName: "price", keyName: "price", name: "Price",
          addMode: EditMode.None, editMode: EditMode.Text, possibleValues: null,
          type: FieldType.Text, args: null
        }
      ],
      editCallback: (viewModel: CoursePricingViewModel) => {
        var model: CoursePricing = new CoursePricing();
        model.categoryId = viewModel.categoryId;
        model.courseId = viewModel.courseId;
        model.price = viewModel.price;

        this.httpService.addCoursePricing(model);
      },
      addCallback: (courseView: CoursePricingViewModel) => {
        if (this.coursePrices
          .findIndex((coursePrice: CoursePricingViewModel) => coursePrice.courseName.toLowerCase() === courseView.courseName.toLowerCase()) !== -1) {
          return;
        }

        let course: Course = {
          id: 0,
          name: courseView.courseName
        };

        this.httpService.addNewCourse(course)
          .then((courseResponse: NewCourseResponse) => {
            let course: Course = courseResponse.course;

            this.coursePrices.push({
              courseId: course.id,
              courseName: course.name,
              price: 0.00,
              categoryId: this.categoryId,
              isEditing: false,
            });
          });
      },
      removeCallback: (course: CoursePricingViewModel) => {
        this.httpService.deleteCourse(course.courseId);
      }
    };
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      let categoryId: number = +params.categoryId;

      this.categoryId = categoryId;

      this.httpService.getPricingCategories()
        .then((response: PricingCategoriesResponse) => {
          let categories = response.categories;
          let category = categories.find((category: PricingCategory) => category.id === categoryId);
          this.header = `Category ${category.name}`;
        });

      Promise.all([this.httpService.getCoursePricing(categoryId), this.httpService.getCourses()])
        .then((response: [CoursePricingResponse, CoursesResponse]) => {
          let coursesPricing: CoursePricing[] = response[0].coursePriceList;
          let courses: Course[] = response[1].courses;

          this.coursePrices = coursesPricing.map((pricing: CoursePricing) => {
            let course: Course = courses.find((course: Course) => course.id === pricing.courseId);

            return {
              courseId: pricing.courseId,
              courseName: course.name,
              price: pricing.price,
              categoryId: pricing.categoryId,
              isEditing: false
            };
          });
        });
    });
  }
}

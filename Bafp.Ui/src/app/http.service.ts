import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { CityCoursesResponse } from './models/responses/cityCourseResponse';
import { CoursesResponse } from './models/responses/coursesResponse';
import { CitiesResponse } from './models/responses/cityResponse';
import { CityDto } from './models/contracts/city';
import { CoursePricingResponse } from './models/responses/coursePricingResponse';
import { CityCourseDto } from './models/contracts/cityCourseDto';
import { PricingCategoriesResponse } from './models/responses/pricingCategoriesResponse';
import { CoursePricing } from './models/contracts/coursePricing';
import { Course } from './models/contracts/course';
import { NewCityResponse } from './models/responses/newCityResponse';
import { NewCourseResponse } from './models/responses/newCourseResponse';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  citiesUrl = "api/cities";
  cityCoursesUrl = "api/cities/{cityId}/courses";
  cityCoursesByCourseUrl = "api/cities/courses/{courseId}";
  coursesUrl = "api/courses";
  deleteCourseUrl = "api/courses/{courseId}";
  addCityCoursesUrl = "api/cities/courses";
  coursesPriceList = "api/courses/prices/{categoryId}";
  coursesPriceListByCourse = "api/courses/{courseId}/prices";
  pricingCategories = "api/pricingCategories";
  coursePricing = "api/courses/prices";

  constructor(private http: HttpClient) { }

  public getCityCourses(cityId: number): Promise<CityCoursesResponse> {
    return this.Wrap(this.http.get<CityCoursesResponse>(environment.apiUrl + this.formatUrl(this.cityCoursesUrl, { cityId })));
  }

  public getCityCoursesByCourse(courseId: number): Promise<CityCoursesResponse> {
    return this.Wrap(this.http.get<CityCoursesResponse>(environment.apiUrl + this.formatUrl(this.cityCoursesByCourseUrl, { courseId })));
  }

  public getCourses(): Promise<CoursesResponse> {
    return this.Wrap(this.http.get<CoursesResponse>(environment.apiUrl + this.coursesUrl));
  }

  public addNewCityCourse(cityCourse: CityCourseDto): Promise<Object> {
    return this.Wrap(this.http.put(environment.apiUrl + this.addCityCoursesUrl, { cityCourse }));
  }

  public addNewCourse(course: Course): Promise<NewCourseResponse> {
    return this.Wrap(this.http.put<NewCourseResponse>(environment.apiUrl + this.coursesUrl, { course }));
  }

  public deleteCourse(courseId: number): Promise<Object> {
    return this.Wrap(this.http.delete(environment.apiUrl + this.formatUrl(this.deleteCourseUrl, { courseId })));
  }

  public getCities(): Promise<CitiesResponse> {
    return this.Wrap(this.http.get<CitiesResponse>(environment.apiUrl + this.citiesUrl));
  }

  public addNewCity(city: CityDto): Promise<NewCityResponse> {
    return this.Wrap(this.http.put<NewCityResponse>(environment.apiUrl + this.citiesUrl, { city }));
  }

  public addCoursePricing(coursePricing: CoursePricing): Promise<Object> {
    return this.Wrap(this.http.put(environment.apiUrl + this.coursePricing, { coursePricing }));
  }

  public getCoursePricing(categoryId: number): Promise<CoursePricingResponse> {
    return this.Wrap(this.http.get<CoursePricingResponse>(environment.apiUrl + this.formatUrl(this.coursesPriceList, { categoryId })));
  }

  public getCoursePricingByCourse(courseId: number): Promise<CoursePricingResponse> {
    return this.Wrap(this.http.get<CoursePricingResponse>(environment.apiUrl + this.formatUrl(this.coursesPriceListByCourse, { courseId })));
  }

  public getPricingCategories(): Promise<PricingCategoriesResponse> {
    return this.Wrap(this.http.get<PricingCategoriesResponse>(environment.apiUrl + this.pricingCategories));
  }

  private formatUrl(url: string, args: Object): string {
    for (const key in args) {
      if (args.hasOwnProperty(key)) {
        const element: string = args[key];
        url = url.replace(`{${key}}`, element);
      }
    }

    return url;
  }

  private Wrap<T>(observable: Observable<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      observable.subscribe((data: T) => resolve(data), (error) => reject(error));
    });
  }
}

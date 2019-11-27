import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { CityCoursesResponse } from './models/cityCourseResponse';
import { CoursesResponse } from './models/coursesResponse';
import { CityCourse } from './models/cityCourse';
import { CityResponse } from './models/cityResponse';
import { CityDto } from './models/city';
import { CoursePricingResponse } from './models/coursePricingResponse';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  citiesUrl = 'api/cities';
  cityCoursesUrl = "api/cities/{cityName}/courses";
  coursesUrl = "api/courses";
  addCityCoursesUrl = "api/cities/courses";
  coursesPriceList = "api/courses/prices/{categoryName}";

  constructor(private http: HttpClient) { }

  public getCityCourses(cityName: string): Promise<CityCoursesResponse> {
    return this.Wrap(this.http.get<CityCoursesResponse>(environment.apiUrl + this.cityCoursesUrl.replace("{cityName}", cityName)));
  }

  public getCourses(): Promise<CoursesResponse> {
    return this.Wrap(this.http.get<CoursesResponse>(environment.apiUrl + this.coursesUrl));
  }

  public addNewCourse(cityCourse: CityCourse): Promise<Object> {
    return this.Wrap(this.http.put(environment.apiUrl + this.addCityCoursesUrl, cityCourse));
  }


  public getCities(): Promise<CityResponse> {
    return this.Wrap(this.http.get<CityResponse>(environment.apiUrl + this.citiesUrl));
  }

  public addNewCity(newCity: CityDto): Promise<Object> {
    return this.Wrap(this.http.put(environment.apiUrl + this.citiesUrl, { city: newCity }));
  }

  public getCoursePricing(pricingCategory: string) : Promise<CoursePricingResponse> {
    return this.Wrap(this.http.get<CoursePricingResponse>(environment.apiUrl + this.coursesPriceList.replace("{categoryName}", pricingCategory)));
  }

  private Wrap<T>(observable: Observable<T>): Promise<T> {
    return new Promise<T>((resolve, reject) => {
      observable.subscribe((data: T) => resolve(data), (error) => reject(error));
    })
  }
}

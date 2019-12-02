import { CookieService } from "./cookieConsent";

export class Constants {
    static get averageStudents(): number {
        let cookieService = new CookieService();

        let averageStudentsCookie = cookieService.getCookie("averageStudents");
        if (averageStudentsCookie) {
            let maybeNumber = Number(averageStudentsCookie);
            if (!isNaN(maybeNumber)) {
                return maybeNumber;
            }
        }

        cookieService.setCookie("averageStudents", "12", 365);
        return 12;
    }

    static set averageStudents(value: number) {
        let cookieService = new CookieService();
        cookieService.setCookie("averageStudents", value.toString(), 365);
    }
}
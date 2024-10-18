import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Person } from '../../models/person/person';
import { GetPeopleResponse } from '../../models/person/getPeopleResponse';
import { GetPersonResponse } from '../../models/person/getPersonResponse';
import { UpdatePerson } from '../../models/person/updatePersonRequest';
import { AddDutyRequest } from '../../models/astronautDuty/addDutyRequest';

@Injectable({
  providedIn: 'root',
})
export class DutyService {
  private apiUrl = environment.apiUrl + '/AstronautDuty';

  constructor(private http: HttpClient) { }

  addDuty(duty: AddDutyRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, duty);
  }
}

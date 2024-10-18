import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Person } from '../../models/person/person';
import { GetPeopleResponse } from '../../models/person/getPeopleResponse';
import { GetPersonResponse } from '../../models/person/getPersonResponse';
import { UpdatePerson } from '../../models/person/updatePersonRequest';

@Injectable({
  providedIn: 'root',
})
export class PersonService {
  private apiUrl = environment.apiUrl + '/Person';

  constructor(private http: HttpClient) { }

  getPeople(): Observable<GetPeopleResponse> {
    return this.http.get<GetPeopleResponse>(this.apiUrl);
  }

  getPersonById(id: number): Observable<GetPersonResponse> {
    return this.http.get<GetPersonResponse>(`${this.apiUrl}/${id}`);
  }

  addOrUpdatePerson(person: UpdatePerson): Observable<UpdatePerson> {
    return this.http.post<any>(`${this.apiUrl}`, person);
  }
}

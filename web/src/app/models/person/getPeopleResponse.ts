import { NetworkResponse } from "../networkResponse";
import { Person } from "./person";

export interface GetPeopleResponse extends NetworkResponse {
  people: [Person];
}

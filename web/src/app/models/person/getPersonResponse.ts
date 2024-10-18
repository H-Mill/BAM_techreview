import { NetworkResponse } from "../networkResponse";
import { Person } from "./person";

export interface GetPersonResponse extends NetworkResponse {
  person: Person;
}

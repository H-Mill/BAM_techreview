import { AstronautDetail } from "../astronautDetail/astronautDetail";
import { AstronautDuty } from "../astronautDuty/astronautDuty";

export interface UpdatePerson {
  id?: number;
  name: string;
  astronautDetail: AstronautDetail;
}

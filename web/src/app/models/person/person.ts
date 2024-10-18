import { AstronautDetail } from "../astronautDetail/astronautDetail";
import { AstronautDuty } from "../astronautDuty/astronautDuty";

export interface Person  {
  id?: number;
  name: string;
  astronautDetail: AstronautDetail;
  astronautDuties: [AstronautDuty]
}

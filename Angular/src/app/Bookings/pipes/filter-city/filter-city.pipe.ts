//***************************************************************************************
//Developer: <Ashita Gaur>
//Create Date: <17th May,2020>
//Last Updated Date: <20th May,2020>
//Description:To perform Business logic and accordingly return response to Bookings.
//Task:To create pipe for filter city in drop down menu of choose city input.
//***************************************************************************************

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'cityFilter'
})
export class FilterCityPipe implements PipeTransform {
  tempvar = [{
    value: "",
    name:
      "Not found"

  }]
  cityArray: any = [];
  transform(items: any, filter: any): any {
    if (!items || !filter) {
      return items;
    }
    else {

      // filter out one name from string
      var temparray = items;
      this.cityArray = temparray.filter(item =>
        item.name.toLowerCase().includes(filter.toLowerCase()));
      if (this.cityArray.length == 0) {
        return this.tempvar;
      }
      else {
        return this.cityArray;
      }

    }
  }

}


import { Pipe, PipeTransform } from '@angular/core';
import { Samples } from './samples';

@Pipe({
    name: 'samplesfilter',
    pure: false
})

export class SampleFilterPipe implements PipeTransform {
    transform(items: Samples[], filter: Samples): Samples[] {
        if(!items || !filter) {
            return items;
        }

        return items.filter((item: Samples) => this.applyFilter(item, filter));
    }

        applyFilter(sample: Samples, filter: Samples): boolean {
            for (let field in filter) {
                if (filter[field]) {
                    if (typeof filter[field] === 'string') {
                        if (sample[field].toLowerCase().indexOf(filter[field].toLowerCase()) === -1) {
                            return false;
                        }
                    } else if (typeof filter[field] === 'number') {
                        if (sample[field] !== filter[field]) {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
}
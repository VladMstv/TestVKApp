export class Utils {
    
    public getHourIntervalString(number) {
        if (number <= 9) {
          return "0" + number + ":00 - " + (number < 9 ? ("0" + (number+1)) : (number+1)) + ":00";
        }
        else return number + ":00 - " + (number+1) + ":00";
      }

    IsEven(n) {
        return n%2 == 0;
    }

    sort(array, descending = true) {
        var len = array.length;
        if(len < 2) { 
          return array;
        }
        var pivot = Math.ceil(len/2);
        if (descending) return this.merge(this.sort(array.slice(0,pivot)), this.sort(array.slice(pivot)));
        else return this.merge(this.sort(array.slice(pivot)), this.sort(array.slice(0,pivot)));
      };
      
    merge(left, right) {
        var result = [];
        while((left.length > 0) && (right.length > 0)) {
          if(left[0] > right[0]) {
            result.push(left.shift());
          }
          else {
            result.push(right.shift());
          }
        }
      
        result = result.concat(left, right);
        return result;
      };
    
    isMax(arr, elem) {
        let len = arr.length;
        let max = -Infinity;
    
        while (len--) {
            max = arr[len] > max ? arr[len] : max;
        }
        
        if (elem == max) return true;
      }
      
    add(a,b) {
        return a+b;
    }
}

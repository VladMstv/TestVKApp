import { Post } from './../models/Post';
import { VkReportServiceService } from './vk-report-service.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, RequiredValidator, Validators, FormControl, FormArray } from '@angular/forms';
import { fbind } from 'q';
import { BehaviorSubject, Subject, of } from 'rxjs';
import { ReportViewModel } from '../view-models/ReportViewModel';
import { catchError } from 'rxjs/operators';


const Hours = [...Array.from(Array(24).keys())];

@Component({
  selector: 'app-vk-report',
  templateUrl: './vk-report.component.html',
  styleUrls: ['./vk-report.component.scss']
})
export class VkReportComponent implements OnInit {

  StepForm: FormGroup;
  Posts: Subject<Post[]> = new Subject();
  Error: string;
  IsLoading: boolean;

  constructor(private fb: FormBuilder, private service: VkReportServiceService) {
    this.InitForm();
    this.Posts.subscribe(x => this.ProcessPosts(x));
   }

  ngOnInit() {
  }

  InitForm() {
    this.StepForm = this.fb.group({
      VKId: ["", Validators.required],
      intervalsArray: this.fb.array([
        new FormControl('')
      ])
    })
    this.StepForm.controls["intervalsArray"] = this.fb.array([]);
    this.StepForm.controls["VKId"].valueChanges.subscribe(x=>{
      if (this.Error) this.Error = null;
    })
  }


  ProcessPosts(posts: Post[]) {
    if (posts.length>0) {
      let numOfPostsInIntervalArray = Array.from({length: 24}, () => 0);
      let likesInIntervalArray = Array.from({length: 24}, () => []);
      let likesMedianInIntervalArray = Array.from({length: 24}, () => 0);
      posts.forEach(post => {
        const hour = new Date(+post.date * 1000).getHours();
        numOfPostsInIntervalArray[hour] = numOfPostsInIntervalArray[hour] + 1;
        likesInIntervalArray[hour].push(post.likes.count);
      });
      
  
      let intervals: ReportViewModel[] = [];
  
      numOfPostsInIntervalArray.forEach((postsNumber, index) => {
        if (postsNumber > 0) {
          let array = Array.from(likesInIntervalArray[index]);
          array = this.sort(array, false);
          //
          if (array.length > 1) {
            if (!this.IsEven(array.length)) {
              likesMedianInIntervalArray[index] = array[((array.length+1)/2)-1];
            }
            else {
              likesMedianInIntervalArray[index] = (array[(array.length/2)-1] + array[(array.length/2+1)-1])/2;
            }
          }
          else {
            if (likesInIntervalArray[index][0]){
              likesMedianInIntervalArray[index] = likesInIntervalArray[index][0];
            }
            else likesMedianInIntervalArray[index] = 0;
          }
          
          (<FormArray>this.StepForm.controls["intervalsArray"]).push(new FormControl({
            HourInterval: this.getHourIntervalString(index),
            PostsCount: postsNumber,
            LikesCount: array.reduce(this.add,0),
            LikesMedian: likesMedianInIntervalArray[index]
        }));
        }
        
      });
    }
  }

  private add(a,b) {
    return a+b;
  }

  getHourIntervalString(number) {
    if (number <= 9) {
      return "0" + number + ":00 - " + (number < 9 ? ("0" + (number+1)) : (number+1)) + ":00";
    }
    else return number + ":00 - " + (number+1) + ":00";
  }

  get VKId() { return this.StepForm.get("VKId") }

  get intervalsArray() { return this.StepForm.get("intervalsArray"); }

  GetVKInfo() {
    if (this.StepForm.valid) {
      this.StepForm.controls["intervalsArray"] = this.fb.array([]);
      this.IsLoading = true;
        this.service.GetVKPosts(this.VKId.value).pipe(
          catchError(err=>{ 
            this.Error = err.message;
            return of([]);
          })
        ).subscribe(x=>{
          this.IsLoading = false;
          this.Posts.next(x);
        });
    }
    this.StepForm.controls["VKId"].reset("");
  }

  IsEven(n) {
    return n%2 == 0;
  }

  private sort(array, descending = true) {
    var len = array.length;
    if(len < 2) { 
      return array;
    }
    var pivot = Math.ceil(len/2);
    if (descending) return this.merge(this.sort(array.slice(0,pivot)), this.sort(array.slice(pivot)));
    else return this.merge(this.sort(array.slice(pivot)), this.sort(array.slice(0,pivot)));
  };
  
  private merge(left, right) {
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

  getMaxOrMinClassForMedian(elem) {
    let models = <ReportViewModel[]>this.intervalsArray.value;
    let arr = models.map(x=>x.LikesMedian);
    let len = arr.length;
    let min = Infinity;
    let max = -Infinity;

    while (len--) {
        min = arr[len] < min ? arr[len] : min;
        max = arr[len] > max ? arr[len] : max;
    }
    if (elem == max) {
      return "max-value";
    }
    else if (elem == min) {
      return "min-value"
    }
    else return "";
  }

  onVKInputKeyDown(event) {
    if (event.keyCode == 13) {
      (event.target as HTMLElement).blur();
      this.GetVKInfo();
    }
  }
}

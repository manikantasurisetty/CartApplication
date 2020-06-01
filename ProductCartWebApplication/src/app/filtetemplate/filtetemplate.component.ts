import { Component, OnInit,Output, Input,EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators,FormArray,FormControl } from '@angular/forms';
import {checkboxList} from '../data';

@Component({
  selector: 'app-filtetemplate',
  templateUrl: './filtetemplate.component.html',
  styleUrls: ['./filtetemplate.component.scss']
})
export class FiltetemplateComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  //checboxlist :any;
  @Input() checboxlist :any;
  @Output() groupFilters =new EventEmitter<any>(null)
  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {

      let newForm = this.formBuilder.group({
      Min: ['', Validators.required],
      Max: ['', Validators.required],
     
      skills:this.formBuilder.array([])

  });
 // this.checboxlist =checkboxList;
 const arrayControl = <FormArray>newForm.controls['skills'];
if(this.checboxlist){
  this.checboxlist.forEach(item => {
    arrayControl.push(new FormControl(false));
});
}       
this.registerForm=newForm;

  }
  submit(filters){
    let skills= filters.skills.map((selected, i) => {
      return {
        // id: this.checboxlist.skills[i].id,
       selected:selected,
        name:this.checboxlist[i]
     }
     
    });
  console.log('test',skills)
  Object.keys(filters).forEach(key => filters[key] === '' || filters[key] ==null ? delete filters[key] : key);
//filters['firstName']='Mani';
filters["skills"]=skills.filter(x=>x.selected);
//filters["skills"]=[{id:1,name:'CSS'}]

console.log('emitdata',filters)

  Object.keys(filters).forEach(key => filters[key] === '' || filters[key] ==null ? delete filters[key] : key);
  //filters['firstName']='Mani';
  filters["skills"]=skills.filter(x=>x.selected);
  
this.groupFilters.emit(filters);
  //filters["skills"]=[{id:1,name:'CSS'}]
  

  }
 
  get skills() {
    return this.registerForm.get('skills');
  };
  
clear(){
  this.registerForm.reset();
  this.submit(this.registerForm.value)
}


}

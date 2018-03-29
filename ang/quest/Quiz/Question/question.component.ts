import { Component, OnInit,Input, Output,EventEmitter }   from '@angular/core';
import { FormGroup,FormControl }      from '@angular/forms';

import {answerComponent} from '../Answer/answer.component';

import {Aw,Qt,Quiz,serviceCl,answerTypes} from '../Model/QtMd.component';


@Component({
  selector: 'question-component'
  ,templateUrl: './question.component.html'
  //,providers:[]
})

export class questionComponent
{
  className: string;

  service_:serviceCl;

  @Input() question_:Qt;
  @Output() saveQuestion_= new EventEmitter<Qt>();

  answers_:Aw[];
  newAnswer_:Aw;

  answerTypes_:answerTypes=new answerTypes();

  constructor(){
    console.log('Constructor st: ' + this.constructor.name)
    this.service_=new serviceCl();
    this.className=this.constructor.name;

    if(
      (typeof(this.question_)!='undefined') &&
      (typeof(this.question_.options)!='undefined')
    ){
      console.log('question options')
      this.answers_= this.question_.options;
      console.log('Question:' + this.question_)
      console.log('answer' + this.question_.options)

    }

    this.newAnswer_=this.service_.newAnswer();
    console.log('questTypes: '); console.log(this.answerTypes_)
    console.log('Constructor fn: ' + this.constructor.name)
  }

  ngOnInit(){
    console.log('Inited: ' + this.constructor.name)
    console.log(this.className);
    console.log(this.answers_);
    console.log(this.newAnswer_);

  }

  addAnswer(a:Aw){
    console.log('addAnswer')
    console.log(a)
    //this.answers_.push(a);
    this.question_.addAnswer(a);
    this.newAnswer_=this.service_.newAnswer();
    console.log(this.question_.options);
  }

  deleteAnswerListen($event){
    console.log('deleteAnswer')
    console.log($event)
    console.log($event.key)
    console.log(this.answers_)
    this.question_.deleteAnswer($event);
    console.log(this.question_.options);
  }

  saveQuestion(q:Qt){
    console.log('saveQuestion')
    console.log(q)
    this.saveQuestion_.emit(q);
  }

  typeChange(s:string){
    console.log('typeChange'+ s)
    this.answerTypes_.bindSelected(s);
  }

}
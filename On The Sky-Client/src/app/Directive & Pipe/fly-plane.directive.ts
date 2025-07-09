import { Directive, ElementRef, EventEmitter, Output, Renderer2, OnInit } from '@angular/core';

@Directive({
  selector: '[appFlyPlane]'
})
export class FlyPlaneDirective implements OnInit {
  @Output() animationDone = new EventEmitter<void>();

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  ngOnInit() {
    this.renderer.addClass(this.el.nativeElement, 'plane');

    // מאזין לסיום האנימציה
    this.el.nativeElement.addEventListener('animationend', () => {
      this.animationDone.emit();
    });
  }
}

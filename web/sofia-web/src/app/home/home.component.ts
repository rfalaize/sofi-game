import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

// helpers
const PI2 = Math.PI * 2;
const random = (min, max) => (Math.random() * (max - min + 1) + min) | 0;
const timestamp = () => new Date().getTime();

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.scss"],
})
export class HomeComponent implements OnInit {
  private canvas: any;
  private ctx: any;
  private then: number;
  private birthday: Birthday;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.canvas = document.getElementById("birthday");
    this.ctx = this.canvas.getContext("2d");
    this.then = timestamp();
    this.birthday = new Birthday(this.canvas, this.ctx);
    window.onresize = () => this.birthday.resize();
    document.onclick = (evt) => this.birthday.onClick(evt);
    document.ontouchstart = (evt) => this.birthday.onClick(evt);
    this.loop();
  }

  loop = () => {
    requestAnimationFrame(this.loop);
    const now: number = timestamp();
    const delta: number = now - this.then;
    this.then = now;
    this.birthday.update(delta / 1000);
  };

  navigateToGame() {
    this.router.navigate(["game"]);
  }
}

class Birthday {
  public fireworks: any[];
  public counter: number;
  private height: number;
  private width: number;
  private center: number;
  private spawnA: number;
  private spawnB: number;
  private spawnC: number;
  private spawnD: number;

  constructor(private canvas: any, private ctx: any) {
    this.resize();
    // create a lovely place to store the firework
    this.fireworks = [];
    this.counter = 0;
  }

  resize() {
    this.width = this.canvas.width = window.innerWidth;
    let center = (this.width / 2) | 0;
    this.spawnA = (this.center - this.center / 4) | 0;
    this.spawnB = (this.center + this.center / 4) | 0;

    this.height = this.canvas.height = window.innerHeight;
    this.spawnC = this.height * 0.1;
    this.spawnD = this.height * 0.5;
  }

  onClick(evt) {
    let x = evt.clientX || (evt.touches && evt.touches[0].pageX);
    let y = evt.clientY || (evt.touches && evt.touches[0].pageY);

    let count = random(3, 5);
    for (let i = 0; i < count; i++)
      this.fireworks.push(
        new Firework(
          this,
          this.ctx,
          random(this.spawnA, this.spawnB),
          this.height,
          x,
          y,
          random(0, 260),
          random(30, 110)
        )
      );

    this.counter = -1;
  }

  update(delta) {
    this.ctx.globalCompositeOperation = "hard-light";
    this.ctx.fillStyle = `rgba(20,20,20,${7 * delta})`;
    this.ctx.fillRect(0, 0, this.width, this.height);

    this.ctx.globalCompositeOperation = "lighter";
    for (let firework of this.fireworks) firework.update(delta);

    // if enough time passed... create new new firework
    this.counter += delta * 3; // each second
    if (this.counter >= 1) {
      this.fireworks.push(
        new Firework(
          this,
          this.ctx,
          random(this.spawnA, this.spawnB),
          this.height,
          random(0, this.width),
          random(this.spawnC, this.spawnD),
          random(0, 360),
          random(30, 110)
        )
      );
      this.counter = 0;
    }

    // remove the dead fireworks
    if (this.fireworks.length > 1000)
      this.fireworks = this.fireworks.filter((firework) => !firework.dead);
  }
}

class Firework {
  private dead: boolean;
  private history: any[];
  private madeChilds: boolean;

  constructor(
    private birthday: Birthday,
    private ctx: any,
    private x: number,
    private y: number,
    private targetX: number,
    private targetY: number,
    private shade: number,
    private offsprings: number
  ) {
    this.dead = false;
    this.history = [];
  }
  update(delta) {
    if (this.dead) return;

    let xDiff = this.targetX - this.x;
    let yDiff = this.targetY - this.y;
    if (Math.abs(xDiff) > 3 || Math.abs(yDiff) > 3) {
      // is still moving
      this.x += xDiff * 2 * delta;
      this.y += yDiff * 2 * delta;

      this.history.push({
        x: this.x,
        y: this.y,
      });

      if (this.history.length > 20) this.history.shift();
    } else {
      if (this.offsprings && !this.madeChilds) {
        let babies = this.offsprings / 2;
        for (let i = 0; i < babies; i++) {
          let targetX =
            (this.x + this.offsprings * Math.cos((PI2 * i) / babies)) | 0;
          let targetY =
            (this.y + this.offsprings * Math.sin((PI2 * i) / babies)) | 0;

          this.birthday.fireworks.push(
            new Firework(
              this.birthday,
              this.ctx,
              this.x,
              this.y,
              targetX,
              targetY,
              this.shade,
              0
            )
          );
        }
      }
      this.madeChilds = true;
      this.history.shift();
    }

    if (this.history.length === 0) this.dead = true;
    else if (this.offsprings) {
      for (let i = 0; this.history.length > i; i++) {
        let point = this.history[i];
        this.ctx.beginPath();
        this.ctx.fillStyle = "hsl(" + this.shade + ",100%," + i + "%)";
        this.ctx.arc(point.x, point.y, 1, 0, PI2, false);
        this.ctx.fill();
      }
    } else {
      this.ctx.beginPath();
      this.ctx.fillStyle = "hsl(" + this.shade + ",100%,50%)";
      this.ctx.arc(this.x, this.y, 1, 0, PI2, false);
      this.ctx.fill();
    }
  }
}

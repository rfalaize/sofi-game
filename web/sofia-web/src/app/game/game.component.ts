import { Component, OnInit } from "@angular/core";
declare var UnityLoader: any;
declare var UnityProgress: any;

@Component({
  selector: "app-game",
  templateUrl: "./game.component.html",
  styleUrls: ["./game.component.scss"],
})
export class GameComponent implements OnInit {
  private gameInstance: any;

  constructor() {}

  ngOnInit() {}

  ngAfterViewInit() {
    this.gameInstance = UnityLoader.instantiate(
      "gameContainer",
      "assets/unity/Build.json",
      { onProgress: UnityProgress }
    );
  }
}

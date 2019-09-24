import React, { Component } from "react";

export default class Grid extends Component {
  constructor(props) {
    super(props);
    this.state = {
      xMaxCells: 15,
      yMaxCells: 15,
      cellSize: 20,
      pointColor: "rgb(30,144,255)"
    };
  }
  isDrawable = object => {
    if (object.X != null) {
      //Object is a point
      return (
        object.X < this.state.xMaxCells &&
        object.X >= 0 &&
        object.Y < this.state.yMaxCells &&
        object.Y >= 0
      );
    } else {
      var isDrawable = true;
      //Object is a square
      //All 4 points must be in range to draw
      object.Points.forEach(point => {
        isDrawable = this.isDrawable && this.isPointDrawable(point);
      });
      return isDrawable;
    }
  };
  isPointDrawable = point => {
    return (
      point.X < Grid.xMaxCells &&
      point.X >= 0 &&
      point.Y < Grid.yMaxCells &&
      point.Y >= 0
    );
  };
  componentDidMount = () => {
    this.drawCanvas();
    this.drawPoints();
    this.drawSquares(this.props.squares);
  };
  componentDidUpdate() {
    this.drawCanvas();
    this.drawPoints();
    this.drawSquares(this.props.squares);
  }
  drawSquares = squares => {
    var ctx = document.getElementById("canvas").getContext("2d");
    ctx.fillStyle = Grid.pointColor;

    squares
      .filter(function(square) {
        return this.isDrawable(square);
      })
      .forEach(function(square) {
        ctx.beginPath();
        ctx.strokeStyle = Grid.pointColor;
        ctx.moveTo(
          square.Points[0].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[0].Y) * Grid.cellSize
        );
        ctx.lineTo(
          square.Points[1].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[1].Y) * Grid.cellSize
        );
        ctx.stroke();

        ctx.moveTo(
          square.Points[0].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[0].Y) * Grid.cellSize
        );
        ctx.lineTo(
          square.Points[2].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[2].Y) * Grid.cellSize
        );
        ctx.stroke();

        ctx.moveTo(
          square.Points[1].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[1].Y) * Grid.cellSize
        );
        ctx.lineTo(
          square.Points[3].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[3].Y) * Grid.cellSize
        );
        ctx.stroke();

        ctx.moveTo(
          square.Points[2].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[2].Y) * Grid.cellSize
        );
        ctx.lineTo(
          square.Points[3].X * Grid.cellSize,
          (Grid.yMaxCells - square.Points[3].Y) * Grid.cellSize
        );
        ctx.stroke();
      });
  };
  drawPoints = () => {
    var ctx = document.getElementById("canvas").getContext("2d");
    ctx.fillStyle = this.state.pointColor;

    this.props.points.forEach(point => {
      ctx.beginPath();
      ctx.arc(
        point.x * this.state.cellSize,
        (this.state.yMaxCells - point.y) * this.state.cellSize,
        5,
        0,
        Math.PI * 2,
        true
      );
      ctx.closePath();
      ctx.fill();
    });
  };
  drawCanvas = () => {
    var ctx = this.refs.canvas.getContext("2d");

    var width = this.state.xMaxCells * this.state.cellSize;
    var height = this.state.yMaxCells * this.state.cellSize;

    ctx.canvas.width = width;
    ctx.canvas.height = height;

    for (var x = 0; x <= width; x += this.state.cellSize) {
      for (var y = 0; y <= height; y += this.state.cellSize) {
        ctx.moveTo(x, 0);
        ctx.lineTo(x, height);
        ctx.stroke();
        ctx.moveTo(0, y);
        ctx.lineTo(width, y);
        ctx.stroke();
      }
    }
  };

  addNewPoint(event) {
    var canvas = document.getElementById("canvas");
    var rect = canvas.getBoundingClientRect();
    var x = Math.round((event.clientX - rect.left) / Grid.cellSize);
    var y = Math.round(
      Grid.yMaxCells - (event.clientY - rect.top) / Grid.cellSize
    );
    var pointObject = {
      x: x,
      y: y,
      pointListId: 0
    };
    this.props.store.addPoint(pointObject);
  }
  render() {
    return (
      <div id="grid">
        <h3 className="h3 text-center">Grid</h3>
        <p />
        <canvas
          ref="canvas"
          id="canvas"
          width="400"
          height="400"
          onClick={this.addNewPoint}
        ></canvas>
        <br />
        <small className="text-muted">
          Click on the grid to add new points. Points, squares which do not fit
          on Grid will not be drawn.
        </small>
      </div>
    );
  }
}

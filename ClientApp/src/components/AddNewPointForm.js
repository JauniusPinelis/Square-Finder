import React, { Component } from "react";

export default class AddNewPointForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      x: null,
      y: null,
      max: 5000,
      min: -5000
    };
  }
  setX = e => {
    this.setState({ x: e.target.value });
  };
  setY = e => {
    this.setState({ y: e.target.value });
  };
  handleSubmit = e => {
    e.preventDefault();
    var x = parseFloat(this.state.x);
    var y = parseFloat(this.state.y);

    if (
      !isNaN(x) &&
      isFinite(y) &&
      !isNaN(x) &&
      isFinite(y) &&
      x < this.state.max &&
      x > this.state.min &&
      y < this.state.max &&
      y > this.state.min
    ) {
      var newPoint = {
        x: x,
        y: y,
        pointListId: 0
      };
      this.props.addPoint(newPoint);
    } else {
      alert(
        "Coordinates must be between " +
          this.state.min +
          " and " +
          this.state.max
      );
    }
  };
  render() {
    return (
      <div id="addNewPointMenu">
        <h3 className="h3">Add New Point</h3>
        <form onSubmit={this.handleSubmit}>
          <input
            className="form-control"
            placeholder="X coordinate"
            onChange={this.setX}
          />
          <small className="text-muted">
            Must be between {this.state.min} and {this.state.max}.
          </small>
          <p />
          <input
            className="form-control"
            placeholder="Y coordinate"
            onChange={this.setY}
          />
          <small className="text-muted">
            Must be between {this.state.min} and {this.state.max}.
          </small>
          <p />
          <button className="btn btn-primary">Add New Point</button>
        </form>
      </div>
    );
  }
}

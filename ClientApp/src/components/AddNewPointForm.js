import React, { Component } from "react";

export default class AddNewPointForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      statics: {
        x: null,
        y: null,
        upperLimit: 5000,
        lowerLimit: -5000
      }
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
      x < this.state.upperLimit &&
      x > this.state.lowerLimit &&
      y < this.state.upperLimit &&
      y > this.state.lowerLimit
    ) {
      var newPoint = {
        x: x,
        y: y
      };
      this.props.addPoint(newPoint);
    } else {
      alert(
        "Coordinates must be between " +
          AddNewPointForm.cordLowerLimit +
          " and " +
          AddNewPointForm.cordUpperLimit
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
          <small className="text-muted">Must be between -5000 and 5000.</small>
          <p />
          <input
            className="form-control"
            placeholder="Y coordinate"
            onChange={this.setY}
          />
          <small className="text-muted">Must be between -5000 and 5000.</small>
          <p />
          <button className="btn btn-primary">Add New Point</button>
        </form>
      </div>
    );
  }
}

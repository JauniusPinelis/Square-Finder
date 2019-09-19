import React, { Component } from "react";

export default class AddNewPointForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      statics: {
        cordUpperLimit: 5000,
        cordLowerLimit: -5000
      }
    };
  }
  getInitialState() {
    return { X: 0, Y: 0 };
  }
  setXCoordinate(e) {
    this.setState({ X: e.target.value });
  }
  setYCoordinate(e) {
    this.setState({ Y: e.target.value });
  }
  handleSubmit(e) {
    e.preventDefault();
    var x = parseFloat(this.state.X);
    var y = parseFloat(this.state.Y);

    if (
      !isNaN(x) &&
      isFinite(y) &&
      !isNaN(x) &&
      isFinite(y) &&
      x < AddNewPointForm.cordUpperLimit &&
      x > AddNewPointForm.cordLowerLimit &&
      y < AddNewPointForm.cordUpperLimit &&
      y > AddNewPointForm.cordLowerLimit
    ) {
      var newPoint = {
        X: this.state.X,
        Y: this.state.Y
      };
      this.props.store.addPoint(newPoint);
    } else {
      alert(
        "Coordinates must be between " +
          AddNewPointForm.cordLowerLimit +
          " and " +
          AddNewPointForm.cordUpperLimit
      );
    }
  }
  render() {
    return (
      <div id="addNewPointMenu">
        <h3 className="h3">Add New Point</h3>
        <form onSubmit={this.handleSubmit}>
          <input
            className="form-control"
            placeholder="X coordinate"
            onChange={this.setXCoordinate}
          />
          <small className="text-muted">Must be between -5000 and 5000.</small>
          <p />
          <input
            className="form-control"
            placeholder="Y coordinate"
            onChange={this.setYCoordinate}
          />
          <small className="text-muted">Must be between -5000 and 5000.</small>
          <p />
          <button className="btn btn-primary">Add New Point</button>
        </form>
      </div>
    );
  }
}

import React, { Component } from "react";

export default class PointListRow extends Component {
  deletePointList() {
    this.props.store.deletePointList(this.props.pointList.Id);
  }
  loadPointList() {
    this.props.store.loadPointList(this.props.pointList.Id);
  }
  render() {
    var pointList = this.props.pointList;
    if (pointList.Id === this.props.state.currentListId)
      return (
        <tr>
          <th scope="row">{pointList.Id}</th>
          <td>{pointList.Name}</td>
          <td>
            <button
              className="btn btn-primary"
              disabled="disabled"
              ref="loadButton"
              onClick={this.loadPointList}
            >
              Loaded
            </button>
            <button className="btn btn-danger" onClick={this.deletePointList}>
              Delete
            </button>
          </td>
        </tr>
      );
    else {
      return (
        <tr>
          <th scope="row">{pointList.Id}</th>
          <td>{pointList.Name}</td>
          <td>
            <button
              className="btn btn-primary"
              ref="loadButton"
              onClick={this.loadPointList}
            >
              Load
            </button>
            <button className="btn btn-danger" onClick={this.deletePointList}>
              Delete
            </button>
          </td>
        </tr>
      );
    }
  }
}

import React, { Component } from "react";

export default class PointRow extends Component {
  deletePoint() {
    this.props.store.deletePoint(this.props.point.Id);
  }
  render() {
    var point = this.props.point;
    return (
      <tr>
        <th scope="row">{point.id}</th>
        <td>{point.x}</td>
        <td>{point.y}</td>
        <td>
          <button className="btn btn-danger" onClick={this.deletePoint}>
            Delete
          </button>
        </td>
      </tr>
    );
  }
}

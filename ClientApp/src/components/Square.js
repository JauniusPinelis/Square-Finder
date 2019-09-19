import React, { Component } from "react";

export default class Square extends Component {
  render() {
    var square = this.props.square;
    return (
      <tr>
        <td>
          X: {square.Points[0].X} <br /> Y: {square.Points[0].Y}{" "}
        </td>
        <td>
          X: {square.Points[1].X} <br /> Y: {square.Points[1].Y}
        </td>
        <td>
          X: {square.Points[2].X} <br /> Y: {square.Points[2].Y}
        </td>
        <td>
          X: {square.Points[3].X} <br /> Y: {square.Points[3].Y}
        </td>
      </tr>
    );
  }
}

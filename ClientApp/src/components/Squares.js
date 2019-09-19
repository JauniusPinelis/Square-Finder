import React, { Component } from "react";
import Square from "./Square";

export default class Squares extends Component {
  render() {
    var squares = this.props.squares.map(function(square) {
      return <Square key={square.Id} square={square} />;
    });
    return (
      <div id="pointsList">
        <h3 className="text-center">Squares</h3>
        <div className="panel-body table-responsive">
          <table className="table table-bordered">
            <thead>
              <tr>
                <th>Point 1</th>
                <th>Point 2</th>
                <th>Point 3</th>
                <th>Point 4</th>
              </tr>
            </thead>
            <tbody>{squares}</tbody>
          </table>
        </div>
      </div>
    );
  }
}

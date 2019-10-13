import React, { Component } from "react";
import PointRow from "./PointRow";

export default class Points extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    var points = this.props.points.map((point) => {
      return <PointRow key={point.id} point={point} deletePoint={this.props.deletePoint} />;
    });
    return (
      <div id="pointsList">
        <h3 className="text-center">Points</h3>
        <div className="panel-body table-responsive">
          <table className="table table-bordered">
            <thead>
              <tr>
                <th>ID</th>
                <th>X</th>
                <th>Y</th>
                <th>Functions</th>
              </tr>
            </thead>
            <tbody>{points}</tbody>
          </table>
        </div>
      </div>
    );
  }
}

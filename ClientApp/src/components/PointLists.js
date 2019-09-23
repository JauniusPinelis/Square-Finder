import React, { Component } from "react";

import PointListRow from "./PointListRow";

export default class PointLists extends Component {
  constructor(props) {
    super(props);
    this.state = {
      xMaxCells: 15,
      yMaxCells: 15,
      cellSize: 20,
      pointColor: "rgb(30,144,255)",
      name: null
    };
  }
  addPointList() {
    this.props.addPointList(this.state.name, this.props.state.points);
  }
  setPointListName(e) {
    this.setState({ name: e.target.value });
  }
  render() {
    var pointListRows = this.props.pointLists.map(function(pointList) {
      return <PointListRow key={pointList.Id} pointList={pointList} />;
    });
    return (
      <div id="pointList">
        <h3 className="text-center">PointLists</h3>
        <div className="form-group">
          <input
            className="form-control"
            placeholder="Name of new PointList"
            onChange={this.setPointListName}
          />
          <small id="passwordHelpInline" className="text-muted">
            Current Points will be saved into new PointList. Using same name
            will overwrite the PointList.
          </small>
          <p />
          <button onClick={this.addPointList} className="btn btn-primary">
            Save as a PointList
          </button>
        </div>

        <p />
        <div className="panel-body table-responsive">
          <table className="table table-bordered">
            <thead>
              <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Functions</th>
              </tr>
            </thead>
            <tbody>{pointListRows}</tbody>
          </table>
        </div>
      </div>
    );
  }
}

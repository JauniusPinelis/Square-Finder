import React, { Component } from "react";

export default class PointLists extends Component {
  constructor(props) {
    super(props);
    this.state = {
      xMaxCells: 15,
      yMaxCells: 15,
      cellSize: 20,
      pointColor: "rgb(30,144,255)"
    };
  }

  getInitialState() {
    return {
      pointlists: [],
      name: ""
    };
  }
  addPointList() {
    this.props.store.addPointList(this.state.name, this.props.state.points);
  }
  setPointListName(e) {
    this.setState({ name: e.target.value });
  }
  render() {
    var store = this.props.store;
    var state = this.props.state;
    var pointListData = state.pointLists.slice();
    //Dont display the default pointlist
    pointListData.shift();
    var pointLists = pointListData.map(function(pointList) {
      return (
        <PointList
          key={pointList.Id}
          pointList={pointList}
          state={state}
          store={store}
        />
      );
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
            Current Points will be saved into new PointList.
            <br />
            Using same name will overwrite the PointList.
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
            <tbody>{pointLists}</tbody>
          </table>
        </div>
      </div>
    );
  }
}

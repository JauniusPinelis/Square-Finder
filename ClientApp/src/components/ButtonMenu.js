import React, { Component } from "react";
import FormUpload from "./FormUpload";

export default class ButtonMenu extends Component {
  constructor(props) {
    super(props);
    this.state = {
      currentListId: 0
    };
  }
  render() {
    return (
      <div id="buttons">
        <h2 className="h3 text-center">Other</h2>
        <button
          className="btn btn-primary"
          onClick={this.props.deleteAllPoints}
        >
          Delete all Points
        </button>
        <p />
        <a
          href={"./api/File/" + this.state.currentListId}
          className="btn btn-primary"
          role="button"
        >
          Export Points
        </a>
        <p />
        <FormUpload />
      </div>
    );
  }
}

import React, { Component } from "react";

export default class FormUpload extends Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedOption: "upload"
    };
  }
  uploadFileToAddPoints(e) {
    e.preventDefault();
    var fd = new FormData();

    var fileToUpload = this.refs.file.files[0];
    if (fileToUpload != null) {
      fd.append("currentListId", this.props.state.currentListId);
      fd.append("file", fileToUpload);
      if (this.state.selectedOption === "upload")
        this.props.store.uploadPointsToAdd(fd);
      else {
        this.props.store.uploadPointsToDelete(fd);
      }
    } else {
      alert("Please select a file");
    }
  }
  handleOptionChange(changeEvent) {
    this.setState({
      selectedOption: changeEvent.target.value
    });
  }
  render() {
    return (
      <div>
        <form encType="multipart/form-data">
          <div className="form-group">
            <input
              ref="file"
              type="file"
              className="btn btn-default btn-primary"
            />
            <p />
            <div className="radio">
              <label>
                <input
                  type="radio"
                  name="fileUpload"
                  value="upload"
                  checked={this.state.selectedOption === "upload"}
                  onChange={this.handleOptionChange}
                />
                Upload
              </label>
            </div>
            <div className="radio">
              <label>
                <input
                  type="radio"
                  name="fileUpload"
                  value="delete"
                  checked={this.state.selectedOption === "delete"}
                  onChange={this.handleOptionChange}
                />
                Delete
              </label>
            </div>
            <input
              type="button"
              ref="button"
              className="btn btn-primary"
              value="Upload"
              onClick={this.uploadFileToAddPoints}
            />
          </div>
        </form>
      </div>
    );
  }
}

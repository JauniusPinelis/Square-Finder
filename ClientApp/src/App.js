/*Libraries */
import React, { Component } from "react";
import axios from "axios";

/* css */
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

/* local components */
import Grid from "./components/Grid";
import Points from "./components/Points";
import Squares from "./components/Squares";
import PointLists from "./components/PointLists";
import AddNewPointForm from "./components/AddNewPointForm";
import ButtonMenu from "./components/ButtonMenu";

/* React-bootstrap components */
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

class App extends Component {
  constructor() {
    super();
    this.state = {
      squares: [],
      points: []
    };

    this.loadData();
  }
  loadData() {
    axios.get("/api/point").then(res => {
      console.log(res);
      console.log(res.data);
      this.setState({
        pointLists: res.data.pointLists || [],
        squares: res.data.squares || [],
        points: res.data.points || []
      });
    });
  }
  deleteAllPoint() {}
  render() {
    return (
      <div className="container">
        <Container>
          <Row>
            <div className="text-center">
              <h1 className="h1">
                Number of squares: {this.state.squares.length}
              </h1>
            </div>
          </Row>
          <Row>
            <Col>
              <Grid />
            </Col>
            <Col>
              <Points points={this.state.points} />
            </Col>
            <Col>
              <Squares squares={this.state.squares} />
            </Col>
          </Row>

          <Row>
            <Col>
              <PointLists />
            </Col>
            <Col>
              <AddNewPointForm />
            </Col>
            <Col>{<ButtonMenu />}</Col>
          </Row>
        </Container>
      </div>
    );
  }
}

export default App;

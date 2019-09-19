var DataStore = function () {

    var callbacks = [];

    var currentListId = 0;

    var ajaxCall = function(url, data, type, processData, contentType, success, error){
        $.ajax({
            url: url,
            data: data,
            processData: processData,
            contentType: contentType,
            async: false,
            type: type,
            success: success,
            error: error
        });

        raiseChanged();

    };

    var getState = function () {

        var points = [];
        var squares = [];
        var pointLists = [];

         $.ajax({
            method: 'GET',
            url: './api/Point/' + currentListId,
            async: false,
            success: function (data) {
                points = data.Points;
                squares = data.Squares;
                pointLists = data.PointLists;
            },
            error: function (xhr){
                alert(xhr.status + " : " + xhr.statusText);
            }    
         });

         var state = {
             points: points,
             squares: squares,
             pointLists: pointLists,
             currentListId : currentListId
        };

        return state;
    };

    var uploadPointsToAdd = function (data) {
        
        ajaxCall('./api/File/', data, 'POST', false, false,
            function(e) {
                if (e)
                    alert(e);
            },
            null);
    };

    var uploadPointsToDelete = function (data) {
        ajaxCall('./api/File/', data, 'DELETE', false, false,
            function(e) {
                if (e)
                    alert(e);
            }, null);
    };

    var addPointList = function (pointListName, data) {
        var pointList = {
            Name: pointListName,
            Points: data
        };

        ajaxCall('./api/PointList/', pointList, 'POST', true, "application/x-www-form-urlencoded", null, null);
    };

    var deletePointList = function (id) {

        ajaxCall('./api/PointList/' + id, '', 'DELETE', false, false, null, null);

        // Check if current pointset was deleted: move to default
        if (currentListId === id)
            currentListId = 1;

        raiseChanged();
    };

    var loadPointList = function (id) {
        currentListId = id;
        raiseChanged();
    };

    var addPoint = function (point) {
        var pointData = {
            X: point.X,
            Y: point.Y,
            PointListId: currentListId
        };

        ajaxCall('./api/Point/', pointData,'POST', true, "application/x-www-form-urlencoded", null,
            function(err) {
                alert(err.responseText);
            });
    };
    var deleteAllPoints = function () {
        ajaxCall('./api/Point/' + currentListId, '', 'PUT', null, null);
    };
    var deletePoint = function (id) {
        ajaxCall('./api/Point/' + id, '', 'DELETE', false, "json", "application/json; charset=utf-8", null, null);
    };
    var addListener = function (callback) {
        callbacks.push(callback);
    };

    var raiseChanged = function () {
        for (var cb of callbacks) {
            cb();
        }
    };

    return {
        addPointList,
        loadPointList,
        deletePointList,
        uploadPointsToAdd,
        uploadPointsToDelete,
        getState,
        addPoint,
        deletePoint,
        deleteAllPoints,
        addListener
    };
};

var store = DataStore();

var App = React.createClass({
    getInitialState: function () {
        //creating and loading initial PointList
        store.addPointList('Default', []);
        store.loadPointList(1);

        return store.getState();
    },
    componentDidMount: function () {
        store.addListener(this.onStoreChange);
    },
    componentWillUnmount: function(){
        store.removeListener(this.onStoreChange);
    },
    onStoreChange: function(){
        var newState = store.getState();
        this.setState(newState);
    },
    render: function () {

        return (
        <div>
            <div className="text-center">
                <h1 className="h1">Number of squares: {this.state.squares.length}</h1>
            </div>
            <div className="jumbotron">
                <div className="row">
                    <div className="col-md-4">
                        <Grid store={store} state={this.state} />
                    </div>
                      <div className="col-md-4">
                        <Points store={store} state={this.state} />
                      </div>
                    <div className="col-md-4">
                        <Squares store={store} state={this.state} />
                    </div>
                </div>
                <p/>
                <div className="row">
                    <div className="col-md-4">
                        <PointLists store={store} state={this.state} />
                    </div>
                    <div className="col-md-4">
                        <AddNewPointForm store={store} state={this.state} />
                    </div>
                     <div className="col-md-4">
                        <ButtonMenu store={store} state={this.state} />
                     </div>
                </div>
            </div>
        </div> 
       );
    }
});

var PointLists = React.createClass({
    getInitialState: function(){
        return {
            pointlists: [],
            name: ""
        };
    },
    addPointList : function(){
        this.props.store.addPointList(this.state.name, this.props.state.points)
    },
    setPointListName: function(e){
        this.setState({ name: e.target.value });
    },
    render: function () {
        var store = this.props.store;
        var state = this.props.state;
        var pointListData = state.pointLists.slice();
        //Dont display the default pointlist
        pointListData.shift();
        var pointLists = pointListData.map(
            function (pointList) {
                return (<PointList key={pointList.Id} pointList={pointList} state={state} store={store} />);
            }
        );
        return (
              <div id="pointList">
                  <h3 className="text-center">PointLists</h3>
                   <div className="form-group">
                     <input className="form-control" placeholder="Name of new PointList" onChange={this.setPointListName} />
                     <small id="passwordHelpInline" className="text-muted">
                        Current Points will be saved into new PointList.<br/> 
                         Using same name will overwrite the PointList.
                     </small>
                     <p /> 
                     <button onClick={this.addPointList} className="btn btn-primary">Save as a PointList</button>
                     
                   </div>
                  
                  <p/>
                    <div className="panel-body table-responsive">
                      <table className="table table-bordered">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Functions</th>
                                </tr>
                            </thead>
                                <tbody>
                                    {pointLists}
                                </tbody>
                      </table>
                   </div>

               </div> 
        );
    }
});

var PointList = React.createClass({
    deletePointList: function(){
        this.props.store.deletePointList(this.props.pointList.Id);
    },
    loadPointList: function(){
        this.props.store.loadPointList(this.props.pointList.Id);
    },
    render: function () {
        var pointList = this.props.pointList;
        if (pointList.Id === this.props.state.currentListId)
            return(
                <tr>
                    <th scope="row">{pointList.Id}</th>
                    <td>{pointList.Name}</td>
                    <td>
                        <button className="btn btn-primary" disabled="disabled" ref="loadButton" onClick={this.loadPointList}>Loaded</button>
                        <button className="btn btn-danger" onClick={this.deletePointList}>Delete</button>
                    </td>
                </tr>
                );
        else {
            return (
                <tr>
                    <th scope="row">{pointList.Id}</th>
                    <td>{pointList.Name}</td>
                    <td>
                        <button className="btn btn-primary" ref="loadButton" onClick={this.loadPointList}>Load</button>
                        <button className="btn btn-danger" onClick={this.deletePointList}>Delete</button>
                    </td>
                </tr>
            );
        }
    }
});

var Points = React.createClass({
    getDefaultProps: function () {
        return {
            points: []
        };
    },
    getInitialState: function () {
        return {
            points: []
        };
    },
    render: function () {
        var store = this.props.store;
        var state = this.props.state;
        var points = state.points.map(
            function (point) {
                return (<PointRow key={point.Id} point={point} store={store} />);
            }
        );
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
                        <tbody>
                            {points}
                        </tbody>
                  </table>
              </div>
          </div>
        );
    }
});

var Squares = React.createClass({
    getDefaultProps: function () {
        return {
            points: []
        };
    },
    getInitialState: function () {
        return {
            points: []
        };
    },
    render: function () {
        var store = this.props.store;
        var state = this.props.state;
        var squares = state.squares.map(
            function (square) {
                return (<Square key={square.Id} square={square} store={store} />);
            }       
        );
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
                    <tbody>
                        {squares}
                    </tbody>

          </table>
              </div>

          </div>  
            );
    }
});

var PointRow = React.createClass({
    deletePoint: function(){
        this.props.store.deletePoint(this.props.point.Id);
    },
    render: function () {
        var point = this.props.point;
        return (
            <tr>
                <th scope="row">{point.Id}</th>
                <td>{point.X}</td>
                <td>{point.Y}</td>
                <td><button className="btn btn-danger" onClick={this.deletePoint}>Delete</button></td>
            </tr>            
        );
    }
});

var Square = React.createClass({
    render: function () {
        var square = this.props.square;
        return (
            <tr>
                <td>X: {square.Points[0].X} <br/> Y: {square.Points[0].Y} </td>
                <td>X: {square.Points[1].X} <br/> Y: {square.Points[1].Y}</td>
                <td>X: {square.Points[2].X} <br/> Y: {square.Points[2].Y}</td>
                <td>X: {square.Points[3].X} <br/> Y: {square.Points[3].Y}</td>
            </tr>            
        );
    }
});



var Grid = React.createClass({
    statics: {
        xMaxCells: 15,
        yMaxCells: 15,
        cellSize: 20,
        pointColor: "rgb(30,144,255)",
        isDrawable: function (object) {
            if (object.X != null) {
                //Object is a point
                return (
                    object.X < Grid.xMaxCells && object.X >= 0
                    && object.Y < Grid.yMaxCells && object.Y >= 0);
            }
            else {

                var isDrawable = true;
                //Object is a square
                //All 4 points must be in range to draw
                object.Points.forEach(function (point) {
                    isDrawable = isDrawable && Grid.isPointDrawable(point);
                });
                return isDrawable;
            }
        },
        isPointDrawable: function (point) {
            return (
                point.X < Grid.xMaxCells && point.X >= 0
                && point.Y < Grid.yMaxCells && point.Y >= 0
            );
        }
    },
    getInitialState: function () {
        return {
            points: [],
            squares: []
        };
    },
    componentDidMount: function () {
        this.drawCanvas();
        this.drawPoints(this.props.state.points);
        this.drawSquares(this.props.state.squares);
    },
    componentDidUpdate: function() {
        this.drawCanvas();
        this.drawPoints(this.props.state.points);
        this.drawSquares(this.props.state.squares);
    },
    drawSquares: function(squares){
        var ctx = document.getElementById('canvas').getContext('2d');
        ctx.fillStyle = Grid.pointColor;
        
        squares.filter(function(square) {
            return Grid.isDrawable(square);
        }).forEach(function(square) {
            ctx.beginPath();
            ctx.strokeStyle = Grid.pointColor;
            ctx.moveTo(square.Points[0].X * Grid.cellSize, (Grid.yMaxCells - square.Points[0].Y) * Grid.cellSize);
            ctx.lineTo(square.Points[1].X * Grid.cellSize, (Grid.yMaxCells - square.Points[1].Y) * Grid.cellSize);
            ctx.stroke();

            ctx.moveTo(square.Points[0].X * Grid.cellSize, (Grid.yMaxCells - square.Points[0].Y) * Grid.cellSize);
            ctx.lineTo(square.Points[2].X * Grid.cellSize, (Grid.yMaxCells - square.Points[2].Y) * Grid.cellSize);
            ctx.stroke();

            ctx.moveTo(square.Points[1].X * Grid.cellSize, (Grid.yMaxCells - square.Points[1].Y) * Grid.cellSize);
            ctx.lineTo(square.Points[3].X * Grid.cellSize, (Grid.yMaxCells - square.Points[3].Y) * Grid.cellSize);
            ctx.stroke();

            ctx.moveTo(square.Points[2].X * Grid.cellSize, (Grid.yMaxCells - square.Points[2].Y) * Grid.cellSize);
            ctx.lineTo(square.Points[3].X * Grid.cellSize, (Grid.yMaxCells - square.Points[3].Y) * Grid.cellSize);
            ctx.stroke();
        });
    },
    drawPoints: function(points){
        var ctx = document.getElementById('canvas').getContext('2d');
        ctx.fillStyle = Grid.pointColor;

        points.filter(function(point) {
            return Grid.isDrawable(point);
        }).forEach(function(point) {
            ctx.beginPath();
            ctx.arc(point.X * Grid.cellSize, (Grid.yMaxCells - point.Y) * Grid.cellSize, 5, 0, Math.PI * 2, true);
            ctx.closePath();
            ctx.fill();
        });
    },
    drawCanvas: function () {
                
        var ctx = this.refs.canvas.getContext('2d');

        var width = Grid.xMaxCells * Grid.cellSize;
        var height = Grid.yMaxCells * Grid.cellSize;

        ctx.canvas.width = width;
        ctx.canvas.height = height;

        for (x = 0; x <= width; x += Grid.cellSize) {
            for (y = 0; y <= height; y += Grid.cellSize) {
                ctx.moveTo(x, 0);
                ctx.lineTo(x, height);
                ctx.stroke();
                ctx.moveTo(0, y);
                ctx.lineTo(width, y);
                ctx.stroke();
            }
        }
    },
    addNewPoint: function(event){
        var canvas = document.getElementById('canvas');
        var rect = canvas.getBoundingClientRect();
        var x = Math.round((event.clientX - rect.left) / Grid.cellSize);
        var y = Math.round(Grid.yMaxCells - ((event.clientY - rect.top) / Grid.cellSize));
        this.props.store.addPoint({ X: x, Y: y });
    },
    render: function () {
        return (
            <div id="grid">
                <h3 className="h3 text-center">Grid</h3>
                <p/>
                <canvas ref="canvas" id="canvas" width="400" height="400" onClick={this.addNewPoint}></canvas>
                <br/>
                <small className="text-muted">
                    Click on the grid to add new points. 
                    Points, squares which do not fit on Grid will not be drawn.
                </small>
            </div>
        );
    }
});

var ButtonMenu = React.createClass({
    render: function () {
            return (
                <div id="buttons">
                    <h2 className="h3 text-center">Other</h2>
                    <button className="btn btn-primary" onClick={this.props.store.deleteAllPoints} >Delete all Points</button> 
                    <p />
                    <a href={"./api/File/" + this.props.state.currentListId} className="btn btn-primary" role="button">Export Points</a>
                    <p/>
                    <FormUpload store={this.props.store} state={this.props.state} />
                </div>  
            );
        }
});

var FormUpload = React.createClass({
    getInitialState: function(e) {
        return {
            selectedOption: 'upload'
    };
    },
    uploadFileToAddPoints: function (e) {
        e.preventDefault();
        var fd = new FormData();
        
        var fileToUpload = this.refs.file.files[0];
        if (fileToUpload != null) {
            fd.append('currentListId', this.props.state.currentListId);
            fd.append('file', fileToUpload);
            if (this.state.selectedOption === 'upload')
                this.props.store.uploadPointsToAdd(fd);
            else {
                this.props.store.uploadPointsToDelete(fd);
            }
        } else {
            alert("Please select a file");
        }
              
    },
    handleOptionChange: function (changeEvent) {
        this.setState({
            selectedOption: changeEvent.target.value
        });
    },
    render: function() {
        return (
            <div>                
               <form encType="multipart/form-data" >
                   <div className="form-group">
                       <input ref="file" type="file" className="btn btn-default btn-primary" />
                       <p/>
                       <div className="radio">
                            <label><input type="radio" name="fileUpload" value="upload" checked={this.state.selectedOption === 'upload'} 
                                          onChange={this.handleOptionChange}/>Upload</label>
                        </div>
                        <div className="radio">
                            <label><input type="radio" name="fileUpload" value="delete" checked={this.state.selectedOption === 'delete'}
                                          onChange={this.handleOptionChange}/>Delete</label>
                        </div>
                       <input type="button" ref="button" className="btn btn-primary" value="Upload" onClick={this.uploadFileToAddPoints} />
                   </div>
               </form> 
            </div>
        );
    }
});



var AddNewPointForm = React.createClass({
    statics: {
        cordUpperLimit: 5000,
        cordLowerLimit: -5000
    },
    getInitialState: function () {
        return { X: 0, Y: 0};
    },
    setXCoordinate: function(e){
        this.setState({ X: e.target.value });
    },
    setYCoordinate: function(e){
        this.setState({ Y: e.target.value });
    },
    handleSubmit: function(e){
        e.preventDefault();
        var x = parseFloat(this.state.X);
        var y = parseFloat(this.state.Y);

        if (!isNaN(x) && isFinite(y)
            && !isNaN(x) && isFinite(y)
            && x < AddNewPointForm.cordUpperLimit && x > AddNewPointForm.cordLowerLimit
            && y < AddNewPointForm.cordUpperLimit && y > AddNewPointForm.cordLowerLimit) {
            var newPoint = {
                X: this.state.X,
                Y: this.state.Y
            };
            this.props.store.addPoint(newPoint);
        }
        else {
            alert('Coordinates must be between ' + AddNewPointForm.cordLowerLimit + ' and ' + AddNewPointForm.cordUpperLimit);
        }       
    },
    render: function () {
        return (
            <div id="addNewPointMenu">
              <h3 className="h3">Add New Point</h3>
              <form onSubmit={this.handleSubmit}>
                  <input className="form-control" placeholder="X coordinate" onChange={this.setXCoordinate} />
                  <small className="text-muted">
                      Must be between -5000 and 5000.
                  </small>
                  <p/>
                  <input className="form-control" placeholder="Y coordinate" onChange={this.setYCoordinate} />
                  <small className="text-muted">
                      Must be between -5000 and 5000.
                  </small>
                  <p/>
                  <button className="btn btn-primary">Add New Point</button>
              </form> 
          </div> 
        );
    }
});

ReactDOM.render(
    <App />,
    document.getElementById('appContainer')
);
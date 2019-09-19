Square Finder App:

Square Finder is a single page web application which allows users to add points and get lists of squares
which are found among given coordinates. It also has responsive Grid - which allows to add points on click.
Other functionalities include: saving and loading pointlists, exporting points, visual tables of points and squares.

Features:
  Single Page application
  Interactive Grid which adds new points on click
  List of Points added with a function to delete points individually
  Both number and list of squares are displayed
  Functionality to import files which enable user to mass add and delete points
  Functionality to save/load PointLists

Stack:
  Front-end: ReactJs with Bootstrap styling
  Back-end: ASP.NET Rest Web api with EF to MSSQL

Possible Improvements:
  1. The square detection algorithm is slow O(n^3) - the apps quickly becomes slower: The app becomes unresponsive at 10000 points.
  This can also be improved if I used HashSet instead of a List, but getting getHashCode() right is tricky.
  2. The whole React logic is in single App.jsx. I tried to split the code into components using FilePack, but I struggled with configuration and errors.
  So I kept in single file. This isw my very first time writing React - so the code can be improved - I think there should be away how
  to use component reuse to create tables of points, squares and pointlists. Also the React is written without ES6 - so it can upgraded, 
  but then transpiler needs to be used for browser compactability.
  3. The visual design of the app can be improved - I probably should have spent more time on that.
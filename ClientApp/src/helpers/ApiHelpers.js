import axios from "axios";

export function apiCall (url, type,data, callback) {
    axios({
        headers: {
          "Content-Type": "application/json"
        },
        method: type,
        url: url,
  
        data: data
      }).then(res=> {
        callback(res);
      });
  }
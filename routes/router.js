var express = require('express');
var router = express.Router();
var util = require('util');

var db = require('../db');
var con = db.init();

var userTable = 'Users';

router.get("/allusers", function(req, res, next) {
  con.query(util.format("SELECT * FROM %s", userTable), function (error, results, fields)
  {
      if(error)
      {
          throw error;
      }
      res.send(JSON.stringify({
        code : 200,
        message : results
      }));
  });
});

router.post("/register", function(req, res, next) {

  var query = util.format("INSERT INTO %s(ID, PW, Nickname) Values (?, ?, ?);", userTable);
  var values = [req.body.id, req.body.pw, req.body.nickname];

  con.query(query, values, function(error, results, fields)
    {
      if(error)
        {
            throw error;
        }  
        res.send(JSON.stringify({
          code : 200,
          message : "Register Success"
        }));
    });
});

module.exports = router;
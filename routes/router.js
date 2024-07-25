var express = require('express');
var router = express.Router();
var sql = require('../sql');

router.get("/getuserlist", function(req, res, next) {
  sql.getUsers(req, res, next);
});

router.post("/register", function(req, res, next) {
  sql.addUser(req, res, next);
});

module.exports = router;
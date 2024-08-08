var express = require('express');
var router = express.Router();
var sql = require('../sql');

//#region User
router.get("/getuserlist", function(req, res) {
  sql.getUserList(req, res);
});

router.get("/getuserinfo/:id", function(req, res) {
  sql.getUserInfo(req.params, res);
});

router.post("/register", function(req, res) {
  sql.registerUser(req, res);
});

router.get("/login/:user", function(req, res) {
  sql.login(req.params, res);
});
//#endregion

//#region Room
router.get("/getroomlist", function(req, res) {
  sql.getRoomList(req, res);
});

router.post("/createroom", function(req, res) {
  sql.createRoom(req, res);
});
//#endregion

module.exports = router;
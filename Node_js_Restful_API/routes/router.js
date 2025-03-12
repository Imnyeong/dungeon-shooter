var express = require('express');
var router = express.Router();
var sql = require('../sql');

//#region User
router.get("/users", function(req, res) {
  sql.getUserList(req, res);
});

router.get("/users/:id", function(req, res) {
  sql.getUserInfo(req.params, res);
});

router.post("/users", function(req, res) {
  sql.registerUser(req, res);
});

router.get("/users/:user", function(req, res) {
  sql.login(req.params, res);
});
//#endregion

//#region Room
router.get("/rooms", function(req, res) {
  sql.getRoomList(req, res);
});

router.post("/rooms", function(req, res) {
  sql.createRoom(req, res);
});

//router.post("/modifyroom", function(req, res) {
//  sql.modifyRoom(req, res);
//});

router.delete("/rooms", function(req, res) {
  sql.deleteRoom(req, res);
});

router.get("/rooms/:id", function(req, res) {
  sql.getRoomInfo(req.params, res);
});

router.get("/members/:id", function(req, res) {
  sql.getRoomMeber(req.params, res);
});
//#endregion

module.exports = router;
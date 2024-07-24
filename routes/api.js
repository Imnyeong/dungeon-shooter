var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/get', function(req, res, next) {
  let name = req.query.name;

  if(name == null || name.length == 0)
  {
    res.send(JSON.stringify({
      code : 400,
      message : "wrong name"
    }));
    return;
  }
  
  res.send(JSON.stringify({
    code : 200,
    name : name,
  }));
});

module.exports = router;

var util = require('util');
var action = require('./action');
var db = require('./db');

var con = db.init();

var userTable = 'Users';

module.exports = 
{
    getUsers : function(req, res, next)
    {
        var query = util.format("SELECT * FROM %s", userTable);

        con.query(query, function (error, results, fields)
        {
            if(error)
            {
                throw error;
            }
            
            action.showUsers(results);

            return res.send(JSON.stringify({
                code : 200,
                message : results
            }));
        });
    },

    addUser : function(req, res, next)
    {
        var query = util.format("INSERT INTO %s(ID, PW, Nickname) Values (?, ?, ?);", userTable);
        var values = [req.body.id, req.body.pw, req.body.nickname];
      
        con.query(query, values, function(error, results, fields)
          {
            if(error)
              {
                  throw error;
              }  

              return res.send(JSON.stringify({
                code : 200,
                message : "Register Success"
              }));
          });
    }
}
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
                message : JSON.stringify(results)
            }));
        });
    },

    addUser : function(req, res, next)
    {
        var query = util.format("INSERT INTO %s(ID, PW, Nickname) Values (?, ?, ?);", userTable);

        var data = req.body[0];
        var values = [data.ID, data.PW, data.Nickname];
        
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
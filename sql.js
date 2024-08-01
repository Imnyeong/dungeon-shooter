var util = require('util');
var action = require('./action');
var db = require('./db');
const { setDefaultResultOrder } = require('dns');

var con = db.init();

var userTable = 'Users';

module.exports = 
{
    getUserList : function(req, res)
    {
        var query = util.format("SELECT * FROM %s", userTable);

        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : JSON.stringify(results)
            }));
        });
    },

    getUserInfo : function(req, res)
    {
        var query = util.format("SELECT * FROM %s WHERE ID = '%s'", userTable, req.id);

        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : JSON.stringify(results)
            }));
        });       
    },

    registerUser : function(req, res)
    {
        var query = util.format("INSERT INTO %s(ID, PW, Nickname) Values (?, ?, ?);", userTable);

        var data = req.body[0];
        var values = [data.ID, data.PW, data.Nickname];
        
        con.query(query, values, function(error)
          {
            if(error)
                  throw error;

              return res.send(JSON.stringify({
                code : 200,
                message : "Register Success"
              }));
          });
    },

    login : function(req, res)
    {
        var user = JSON.parse(req.user);
        var query = util.format("SELECT * FROM %s WHERE ID = '%s' AND PW = '%s'", userTable, user.ID, user.PW);
        
        con.query(query, function(error, results)
          {
            if(error)
                  throw error;

            if(results[0] == null)
            {
                return res.send(JSON.stringify({
                    code : 200,
                    message : 'Login Failed'
                }));
            }
            else
            {
                return res.send(JSON.stringify({
                    code : 200,
                    message : JSON.stringify(results[0])
                }));
            }
          });
    }
}
var util = require('util');
var action = require('./action');
var db = require('./db');
const { setDefaultResultOrder } = require('dns');
const { Console } = require('console');

var con = db.init();

var userTable = 'Users';
var roomTable = 'Rooms';

let maxRoomCount = 10;

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

        var data = req.body;
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
    },

    getRoomList : function(req, res)
    {
        var query = util.format("SELECT * FROM %s", roomTable);

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

    createRoom : function(req, res)
    {
        var searchQuery = util.format("SELECT * FROM %s", roomTable);

        con.query(searchQuery, function (error, results)
        {
            if(error)
                throw error;

            for(let index = 0 ; index < maxRoomCount; index++)
            {
                var find = results.find(x => x.RoomID == index);
                
                if(find == null)
                {
                    var query = util.format("INSERT INTO %s (RoomID, RoomName, MasterID, Players) Values (?, ?, ?, ?);", roomTable);

                    var data = req.body;
                    var values = [index, data.RoomName, data.MasterID, data.Players];
                    
                    con.query(query, values, function(error)
                      {
                        if(error)
                              throw error;
                        //console.log(index.toString());

                        return res.send(JSON.stringify({
                            code : 200,
                            message : index.toString()
                          }));                          
                      });
                    break;
                }
                else
                {
                    if(index == maxRoomCount - 1)
                    {
                        return res.send(JSON.stringify({
                            code : 200,
                            message : "RoomList is Full"
                          }));   
                    }
                }
            }
        });
    },

    modifyRoom : function(req, res)
    {
        var data = req.body;
        var query = util.format("UPDATE %s SET Players = '%s', CanJoin = '%s' WHERE RoomID = '%s';", roomTable, data.Players, data.CanJoin, data.RoomID);
        
        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : "Modify Success"
            }));
        });
    },

    deleteRoom : function(req, res)
    {
        var data = req.body;
        var query = util.format("DELETE FROM %s WHERE RoomID = '%s';", roomTable, data.RoomID);

        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : "Delete Success"
            }));
        });
    },

    getRoomInfo : function(req, res)
    {
        var query = util.format("SELECT * FROM %s WHERE RoomID = '%s'", roomTable, req.id);

        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : JSON.stringify(results[0])
            }));
        });       
    },

    getRoomMeber : function(req, res)
    {
        var query = util.format("SELECT Players FROM %s WHERE RoomID = '%s'", roomTable, req.id);

        con.query(query, function (error, results)
        {
            if(error)
                throw error;

            return res.send(JSON.stringify({
                code : 200,
                message : results[0].Players
            }));
        });       
    }
}
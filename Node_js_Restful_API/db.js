var mysql = require('mysql');

let db_info = 
{

}

module.exports = 
{
    init: function() 
    {
        return mysql.createConnection(db_info);
    },

    connect: function(con)
    {
        con.connect(function(err) 
        {
            if(err)
            {
                console.error('mysql connection error: ' + err);
            }
            else
            {
                console.log('mysql is connected successfully');
            }
        })
    }
}
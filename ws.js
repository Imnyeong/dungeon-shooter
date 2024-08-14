var WebSocket = require('ws');

var wss;

wss.on('connection', (ws, req) =>
{
    if(req.url.startsWith(''))
    {
        ws.location = req.url;
        
        ws.on('message', (message) => 
            {
                wss.clients.forEach(function each(client)
                {
                    if(client != ws && client.location === ws.location)
                    {
                        //var packet = JSON.parse(message);
                        client.send(message.toString());
                    }
                });
            });
    }
    else
    {
        console.log('Wrong Connection');
    }
    ws.on('close', () => 
    {
        console.log('Client Disconnected');
    });
})
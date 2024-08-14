var WebSocket = require('ws');

var wss;

wss.on('connection', (ws, req) =>
{
    console.log(req);
    console.log('Connected');
    if(req.url.startsWith(''))
    {
        //console.log('Connected With Room');
        ws.location = req.url;
        //console.log("ws.location = " + ws.location);
        ws.on('message', (message) => 
            {
                wss.clients.forEach(function each(client)
                {
                    //if(client != ws)
                    if(client === ws.location)
                    {
                        client.send(message.toString());
                    }
                        // 모든 클라이언트에 전송
                        //if(client != ws)
                        //{
                            // 자신을 제외한 클라이언트에 전송
                        //}
                });
                //console.log(`Receiced message: ${message}`);
                //var packet = JSON.parse(message);
                //console.log(`Receiced packetType: ${packet.packetType}`);
                //console.log(`Receiced packetdata: ${packet.data}`);
            });
    }
    else
    {
        console.log('Connected Without Room');
    }

    ws.on('close', () => 
    {
        console.log('Client Disconnected');
    });
})
var WebSocket = require('ws');

var wss;

wss.on('connection', (ws) =>
{
    console.log("New Client Connected");

    ws.on('message', (message) => 
    {
        console.log(`Receiced message: ${message}`);

        wss.clients.forEach(function each(client) {
            // 모든 클라이언트에 전송
            client.send(message.toString());
            if(client != ws)
            {
                // 자신을 제외한 클라이언트에 전송
            }
        });
    });

    ws.on('close', () => 
    {
        console.log('Client Disconnected');
    });
})
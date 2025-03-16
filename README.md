### DUNGEON SHOOTER
![icon](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) ![icon](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![icon](https://img.shields.io/badge/Node.js-43853D?style=for-the-badge&logo=node.js&logoColor=white) ![icon](https://img.shields.io/badge/Firebase-F29D0C?style=for-the-badge&logo=firebase&logoColor=white)

## 개요 📝
Node.js로 WebSocket 통신과 RESTful API를 구현한 멀티플레이 Unity 3D FPS 게임, Firebase DB 사용

## Tech Stack ✏️
- Unity
- C#
- Node.js
- WebSocket
- RESTful API
- Firebase
- Visual Studio
- Sourcetree

## 기술 🔎
- WebSocket 통신으로 클라이언트 간의 실시간 정보 교환
- RESTful API로 유저 데이터를 DB에 저장
- ObjectPooling 으로 무분별한 Instantiate 방지

## Script로 보는 핵심 기능 📰

### ObjectPooling
```ruby
weaponPools = new List<GameObject>[weaponPrefabs.Length];

for (int key = 0; key < weaponPools.Length; key++)
{
    weaponPools[key] = new List<GameObject>();
}
...
public GameObject GetWeapon(int _key)
{
    GameObject result = null;

    foreach (GameObject go in weaponPools[_key])
    {
        if (!go.activeSelf)
        {
            result = go;
            result.SetActive(true);
            break;
        }
    }
    if (result == null)
    {
        result = Instantiate(weaponPrefabs[_key], transform);
        weaponPools[_key].Add(result);
    }
    return result;
}
```

ObjectPool에서 최초에 Prefab들의 정보를 key로 구분하여 가지고있고 필요한 Object를 key로 호출, 이전에 생성된 Object가 남는 경우엔 그 Object를 return하고 개수가 부족할 경우에만 새로 Instantiate하여 return

### RESTful API
**Client**
```ruby
using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
{
    yield return webRequest.SendWebRequest();

    if(webRequest.result == UnityWebRequest.Result.Success)
    {
        Debug.Log("Success Received: " + webRequest.downloadHandler.text);
        if (_action != null)
        {
            WebRequestResponse response = 
            JsonConvert.DeserializeObject<WebRequestResponse>(webRequest.downloadHandler.text);
            _action(response);
        }
    }
    else
    {
        Debug.LogError("Error: " + webRequest.error);
    }
}
```
**Server**
```ruby
router.get("/users", function(req, res) {
  sql.getUserList(req, res);
});
...
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
}
```

Client는 지정된 Server측 URL로 GET Method를 전송하고, Server는 알맞은 경로로 라우팅 후 DB에서 특정 sql문의 결과를 받아 Client측으로 return 합니다.  
**Client측에서 Packet을 전송할 때, using을 사용하면 Request의 응답을 받은 후 메모리를 해제하기 때문에 메모리 낭비를 방지할 수 있습니다.**

### WebSocket 통신
**Client**
```ruby
CharacterPacket packet = new CharacterPacket()
{
    id = id,
    hp = hp,
    position = transform.localPosition,
    rotation = transform.localRotation,
    animation = animController.animationType
};
WebSocketRequest request = new WebSocketRequest()
{
    packetType = PacketType.Character,
    data = JsonUtility.ToJson(packet)
};
WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
```
**Server**
```
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
```

Client에서는 플레이어가 움직일 때 위치 정보, Animation 정보를 Packet으로 만들어서 WebSocket서버로 전송하고 Server 측에선 열린 Socket 내에서 같은 방에 소속된 다른 플레이어들에게 Pakcet을 전송합니다.

## Sample Image 🎮

<img src="https://github.com/user-attachments/assets/e50e4981-c8a8-4c6e-aa51-74134c11cb10" width="480" height="270"/>  
<img src="https://github.com/user-attachments/assets/0808653b-9cf2-4dc4-bf34-6ee785b6ff44" width="480" height="270"/>

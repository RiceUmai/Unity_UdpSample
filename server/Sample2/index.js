const { Console } = require('console');
const dgram = require('dgram');
const server = dgram.createSocket('udp4');

const { v4: uuidv4 } = require('uuid');

const JsonInit = require('./JsonInit.js');

const Port = 6060;

//クライアント情報保存
var clients = [];

server.on('error', (err) => {
  console.log(`server error:\n${err.stack}`);
  server.close();
});

//クライアントからデータを受ける処理
//msg = data
//senderInfo = dataを送ったクライアント情報
server.on('message', (msg, senderInfo) => {
  evectHandler(msg, senderInfo)
});

server.on('listening', () => {
  const address = server.address();
  console.log(`server listening on ${address.address}:${address.port}`);
});

//Port番号設定
server.bind(Port);

//=======================
//=======================
function clientsClear(clients) {

  for (let index in clients) {
    delete clients[index];
  }
  console.log("ClientsClear");
}


//=======================
//Jsonデータを解析してEvect処理
//=======================
function evectHandler(msg, senderInfo) {

  try {

    var data = JSON.parse(msg);

    console.log('Messages received ' + msg)
    var thisPlayerId = uuidv4();

    switch (data.type) {

      case 'init':

        if (Object.keys(clients).length == 0) {
          setTimeout(clientsClear, 5000, clients);
        }

        for (let index in clients) {
          var JsonObj = data;
          JsonObj.ribal.address = clients[index].address;
          JsonObj.ribal.port = clients[index].port;
          Emit(JsonObj, senderInfo);
        }

        var thisPlayerId = uuidv4();
        clients[thisPlayerId] = senderInfo
        console.log(Object.keys(clients).length);
        console.log(senderInfo);


        var JsonObj = data;
        JsonObj.ribal.address = senderInfo.address;
        JsonObj.ribal.port = senderInfo.port;
        //JsonObj.ribal.pos.x = 0.5;
        Broadcast(JsonObj, clients, senderInfo);
        break;

        //case 'anoterEventType': 
        //break;

        default:

        break;
    }
  }

  catch (error) {
    console.log(error);
  }
}

//JsonをStringで変換してクライアントへ送信
function Emit(JsonObj, senderInfo) {

  var msg = JSON.stringify(JsonObj);
  server.send(msg, senderInfo.port, senderInfo.address, () => {
    console.log(`Message sent to ${senderInfo.address}:${senderInfo.port} = ${msg}`)
  })
}

//全クライアントへ送信
function Broadcast(JsonObj, clients, senderInfo) {

  var msg = JSON.stringify(JsonObj);
  for (let index in clients) {

    if (clients[index].port === senderInfo.port)
      continue;

    Emit(JsonObj, clients[index])
  }
}

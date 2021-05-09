const { Console } = require('console');
const dgram = require('dgram');
const server = dgram.createSocket('udp4');

const { v4: uuidv4 } = require('uuid');

const Port = 6060;

var clients = [];

server.on('error', (err) => {
console.log(`server error:\n${err.stack}`);
  server.close();
});

server.on('message', (msg, senderInfo) => {
  evectHandler(msg, senderInfo)
});

// server.on('close', () => {
//   console.log('client has closed, closing server');
// });

server.on('listening', () => {
  const address = server.address();
  console.log(`server listening on ${address.address}:${address.port}`);
});

server.bind(Port);

//=======================
//=======================
function evectHandler(msg, senderInfo){

  try {
      
  var data = JSON.parse(msg);
  
  console.log('Messages received '+ msg)
  var thisPlayerId = uuidv4();

  switch(data.type){
    case 'init':
    var thisPlayerId = uuidv4();
    clients[thisPlayerId] = senderInfo
    console.log(senderInfo);
    

    var JsonObj = { type : 'init', Ribal_ip : senderInfo.address, Ribal_port : senderInfo.port}
    var JsonString = JSON.stringify(JsonObj);

    //Emit(JsonString ,senderInfo);
    Broadcast(JsonString, clients, senderInfo);
    break;
    
    default:
    //   server.send(msg,senderInfo.port,senderInfo.address,()=>{
    //     console.log(`Message sent to ${senderInfo.address}:${senderInfo.port}`)
    //   })
    break;
  }}

  catch (error) {
    console.log(error);
  }
}

function Emit(msg, senderInfo){
  server.send(msg, senderInfo.port, senderInfo.address,()=>{
    console.log(`Message sent to ${senderInfo.address}:${senderInfo.port} = ${msg}`)
  })
}


function Broadcast(msg, clients, senderInfo){
  for(let index in clients){
    
    if(clients[index].port === senderInfo.port){
      continue;
    }

    server.send(msg, clients[index].port, clients[index].address,()=>{
      console.log(`Message sent to ${clients[index].address}:${clients[index].port}`)
    })
  }

  // Object.keys(clients).forEach( function(value) {

  //   console.log( value + '：' + this[value] );
  // }, arr)
}

const { Console } = require('console');
const dgram = require('dgram');
const server = dgram.createSocket('udp4');

const { v4: uuidv4 } = require('uuid');

const Port = 6060;

server.on('error', (err) => {
  console.log(`server error:\n${err.stack}`);
  server.close();
});

server.on('message', (msg, senderInfo) => {
  server.send(msg, senderInfo.port, senderInfo.address, () => {
    console.log(`Message sent to ${senderInfo.address}:${senderInfo.port} = ${msg}`)
  })
});

server.on('listening', () => {
  const address = server.address();
  console.log(`server listening on ${address.address}:${address.port}`);
});

server.bind(Port);
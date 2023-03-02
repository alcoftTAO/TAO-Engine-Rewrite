import socket

# Server variables
TAO_Server_Port = 5325
TAO_Server_Buffer = 1024
TAO_Server_On_Receive = lambda: None

# Server start
TAO_Server_Socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
TAO_Server_Socket.bind(("127.0.0.1", TAO_Server_Port))

# Server listen
TAO_Server_Socket.listen(1)
TAO_Server_Connection, TAO_Server_Address = TAO_Server_Socket.accept()

try:
    # Receive data
    TAO_Server_Data = TAO_Server_Connection.recv(TAO_Server_Buffer)

    # Execute action
    TAO_Server_On_Receive(TAO_Server_Data)
finally:
    # Close the connection
    TAO_Server_Connection.close()

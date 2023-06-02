# lib0t

Servidor sb0t para distribuciones Linux.

Es un servidor experimental, lo que significa que puede estar sujeto a fallos. Sin embargo se han realizado pruebas, las cuales han sido exitosas.

**Problemas actuales que presenta el servidor**
- Usuarios de cb0t/ares o clientes TCP presentan problemas al escribir, los mensajes se cortan.
- Algunos usuarios en general presentan problemas para conectarse, el servidor los rechaza.

## Requisitos

- Docker

### 1 Instalar Docker
Los siguientes pasos son para realizar la instalación de docker en la distribución de Linux Ubuntu, si posee otra distribución referirse a la [documentación de Docker](https://docs.docker.com/engine/install/#server).

#### 1.1 Remover cualquier instalación previa de Docker
Las versiones anteriores de Docker se llamaban docker, docker.io, o docker-engine, también puede tener instalaciones de containerd o runc. Desinstale cualquiera de estas versiones anteriores antes de intentar instalar una nueva versión con el siguiente comando.
```bash
sudo apt-get remove docker docker-engine docker.io containerd runc
```

#### 1.2 Agregar Docker a los repositorios
##### 1.2.1 Actualizar repositorios

```bash
sudo apt-get update
sudo apt-get install ca-certificates curl gnupg
```

#### 1.2.2 Agregue la clave GPG oficial de Docker
```bash
sudo install -m 0755 -d /etc/apt/keyrings

curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg

sudo chmod a+r /etc/apt/keyrings/docker.gpg
```

##### 1.2.3 Agregar Docker a los repositorios de Ubuntu.
```bash
echo "deb [arch="$(dpkg --print-architecture)" signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu "$(. /etc/os-release && echo "$VERSION_CODENAME")" stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```
##### 1.2.4 Actualizar repositorios

```bash
sudo apt-get update
```
### 1.3 Instalar Docker
```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
```
### 1.4 Verificar la instalación de Docker
```bash
sudo docker run hello-world
```
Este comando descarga una imagen de prueba y la ejecuta en un contenedor. Cuando se ejecuta el contenedor, imprime un mensaje de confirmación y termina.


## Crear sala de chat con lib0t - Proceso guiado (gráfico)
La creación de una sala de chat mediante un proceso guiado, se refiere al uso de una aplicación para facilitar la ejecución de comandos descritos en el proceso manual.
Esta aplicación es una aplicación web (página), que permite crear salas de chat y administrarlas. Para ello es necesario la instalación de dos aplicaciones, una aplicación cliente (página) y una aplicación servidor.
Estas aplicaciones han sido empaquetas en imagenes de Docker, para facilitar su instalación, por lo que se requiere de Docker para su instalación.

### 1 Instalar aplicación servidor

Ejecutar el siguiente comando

```
sudo docker run -it -d --name las -v /var/run/docker.sock:/var/run/docker.sock -v /lib0t:/lib0t -p 7000:80 bsjaramillo/las
```
Al ejecutar este comando, se descargará la aplicación servidor y quedará en ejecución lista para usarla.

**Nota:** Se requiere que el puerto de aplicación servidor (7000) esté configurado en el firewall de la vps.

### 2 Instalar aplicación cliente

Ejecutar el siguiente comando

```
sudo docker run -it -d --name lac -p 9000:80 bsjaramillo/lac
```
Al ejecutar este comando, se descargará la aplicación cliente (página) y quedará en ejecución lista para usarla.

Para abrir la aplicación web en cualquier navegador, user la IP del servidor y el puerto que usa la aplicación web.
Ejemplo:
```
http://3.50.100.200:9000
```
**Nota:** Se requiere que el puerto de aplicación cliente (9000) esté configurado en el firewall de la vps.

### 3 Crear usuario administrador
Para poder crear y administar las salas de chat, la aplicación cliente (página) requerirá de un login para poder ejecutar los comandos, por motivos de control y seguridad.

#### 3.1 Crear una base de datos para los usuarios
Ejecutar el siguiente comando.
```
sudo docker exec -it las python3 cli_tool.py -o create_database
```
Si se ejecutó correctamente, esto desplegará un mensaje de que la base de datos ha sido inicializada.

#### 3.2 Crear una clave secreta
Ejecutar el siguiente comando.
```
sudo docker exec -it las python3 cli_tool.py -o generate_secret_key
```
Si se ejecutó correctamente, esto desplegará un mensaje de que se ha creado una clave secreta.

#### 3.3 Crear usuario administrador
Ejecutar el siguiente comando.
```
sudo docker exec -it las python3 cli_tool.py -o generate_admin_user
```
Este comando le pedirá que ingrese un nombre de usuario y una contraseña.

Si se ejecutó correctamente, esto desplegará un mensaje de que se ha creado un usuario administrador.

#### 3.4 Opcional
Si olvida la contraseña puede cambiarla con el siguiente comando.
```
sudo docker exec -it las python3 cli_tool.py -o regenerate_admin_password
```
Este comando le pedirá que ingrese el nombre de usuario y una contraseña nueva.

Si se ejecutó correctamente, esto desplegará un mensaje de que se ha actualizado la contraseña.

## Crear sala de chat con lib0t - Proceso manual
Para facilitar la instalación de este servidor, en cualquier distribución Linux, se ha creado una imagen en docker, que contiene todos los ejecutables necesarios para la creación y ejecución de una sala de chat, por lo cual, se requiere tener instalado Docker.


### 1 Descargar e instalar lib0t
Descargar la imagen de lib0t de los repositorios de Docker
```bash
sudo docker pull bsjaramillo/lib0t
```
Verifica que la imagen se haya descargado
```bash
sudo docker images
```

### 2 Crear el chatroom space para la sala de chat
Con este servidor se da la posibilidad de poder crear multiples salas de chat en una misma máquina. Por lo cual es necesario crear un **chatroom space** o carpeta, que separe los archivos de configuración (templates, motd, scripts, etc) de cada sala de chat.

Primero crear la siguiente carpeta
```bash
sudo mkdir /lib0t
```
Esta carpeta contendrá todos los chatroom spaces de cada sala de chat.
Luego elegir el nombre del **chatroom space** para la sala de chat, elegir un nombre corto y evitar usar caracteres especiales (tildes, simbolos especiales, etc), como ejemplo se creará un **chatroom space** llamado room1.

*sudo mkdir /lib0t/**chatroom space***
```bash
sudo mkdir /lib0t/room1
```
### 3 Descargar archivos de configuración para la sala de chat
Para configurar una sala de chat (nombre, puerto, etc) en Windows se utilizaba la interfaz de usuario, en las distribuciones Linux por lo general no existe o no es posible realizarlo de la misma manera. Por lo cual es necesario descargar y editar unos archivos de configuración en formato JSON.
#### 3.1 Crear la carpeta para los archivos de configuración

*sudo mkdir /lib0t/**chatroom space**/Settings*
```bash
sudo mkdir /lib0t/room1/Settings
```
#### 3.2 Descargar los archivos de configuración
AppSettings.json, este archivo de configuración es necesario para la creación de la sala de chat, en este se establecen los valores o parámetros para el nombre de la sala, puerto, contraseña, habilitar para ib0t, etc.

*sudo wget -O /lib0t/**chatroom space**/Settings/AppSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/AppSettings.json*
```bash
sudo wget -O /lib0t/room1/Settings/AppSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/AppSettings.json
```
CommandsSettings, este archivo de configuración es necesario en tiempo de ejecución de la sala, no es necesario editar.

*sudo wget -O /lib0t/**chatroom space**/Settings/CommandsSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/CommandsSettings.json*
```bash
sudo wget -O /lib0t/room1/Settings/CommandsSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/CommandsSettings.json

```
### 4 Configurar la sala de chat
Para configurar la sala de chat es necesario editar el archivo AppSettings.json previamente descargado. Este archivo está dividido en varias secciones.
```
Configuraciones Principales (MainSettings)
Configuraciones de Administrador (AdminSettings)
Configuraciones de Linkeo (LinkingSettings)
Configuraciones Avanzadas (AdvancedSettings)
Configuraciones de Avatar (AvatarSettings)
Configuraciones Extras (ExtraSettings)
```
Entre los parámetros principales a configurar se detallan a continuación
```
Nombre de la sala => MainSettings => roomName
Puerto => MainSettings => roomPort
Nombre del bot => MainSettings => botName
Contraseña del host => AdminSettings => ownerPassword
```
Por defecto vienen habilitadas las siguientes características principales
```
Scribbles => MainSettings => roomScribblesEnabled
Mostrar la sala en lista de salas => MainSettings => showRoomInChannelList
Habilitado para ib0t => MainSettings => ib0tSupportEnnabled
Comandos Javascript => AdminSettings => enableBuiltInCommands
Ambiente de Javascript => AdvancedSettings => enableJavascriptEngine
Scripting en la sala => AdvancedSettings => enableInRoomScripting 
Scripts pueden cambiar el level => AdvancedSettings => scriptsCanChangeLevel
```
Para editar este archivo ejecutar el siguiente comando

*sudo nano /lib0t/**chatroom space**/Settings/AppSettings.json*
```bash
sudo nano /lib0t/room1/Settings/AppSettings.json
```

Para moverse dentro del archivo utilizar las flechas de dirección del teclado. Una vez terminado de editar el archivo guardar los cambios presionar las teclas "Ctrl+x", luego la tecla "y" y por último dar enter.

### 5 Crear la sala de chat
En este punto recordar el nombre del **chatroom space** elegido en el punto 1.0 y el **puerto** establecido en el archivo de configuración AppSettings.json. Ejecutar el siguiente comando.

*sudo docker run -d -it -v /lib0t/**chatroom space**:/lib0t/**chatroom space** --name **chatroom space** -p **puerto**:**puerto** bsjaramillo/lib0t **chatroom space***
```bash
sudo docker run -d -it -v /lib0t/room1:/lib0t/room1 --name room1 -p 54321:54321 bsjaramillo/lib0t room1
```
Para verificar que la sala ha sido creada con sin problemas, ejectuar el siguiente comando.

*sudo docker logs **chatroom space***
```bash
sudo docker logs room1
```
Al ejecutar este comando debería visualizar un mensaje diciendo que se ha creado la sala en el puerto establecido.

### 6 Iniciar, Apagar, Eliminar, Reinciar sala de chat

Para iniciar la sala de chat

*sudo docker start **chatroom space***
```bash
sudo docker start room1
```
Para apagar la sala de chat

*sudo docker stop **chatroom space***
```bash
sudo docker stop room1
```

Para eliminar la sala de chat

*sudo docker stop **chatroom space***

*sudo docker rm **chatroom space***
```bash
sudo docker stop room1
sudo docker rm room1
```
Para reinciar la sala de chat, este comando se puede utilizar cuando se realiza un cambio en el archivo de configuración AppSettings.json y es necesario reinciar el servidor para aplicar los cambios.

*sudo docker restart **chatroom space***
```bash
sudo docker restart room1
```

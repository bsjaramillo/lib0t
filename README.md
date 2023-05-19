
# lib0t

Servidor sb0t para distribuciones Linux. Es un servidor experimental, lo que significa que puede estar sujeto a fallos. Sin embargo se han realizado pruebas, las cuales han sido satisfactorias.




## Instalación
Para facilitar la instalación de este servidor, en cualquier distribución Linux, se ha creado una imagen en docker, que contiene todos los ejecutables necesarios para la creación y ejecución de una sala de chat.

### 1. Instalar Docker
Los siguientes pasos son para realizar la instalación de docker en la distribución de Linux Ubuntu, si posee otra distribución referirse a la documentación de [documentación de Docker](https://docs.docker.com/engine/install/#server).

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
##### 1.2.2 Agregue la clave GPG oficial de Docker
```bash
echo "deb [arch="$(dpkg --print-architecture)" signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu "$(. /etc/os-release && echo "$VERSION_CODENAME")" stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
```
##### 1.2.3 Actualizar repositorios

```bash
sudo apt-get update
```
### 1.3 Installar Docker
```bash
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin
```
### 1.3 Verificar la instalación de Docker
```bash
sudo docker run hello-world
```
Este comando descarga una imagen de prueba y la ejecuta en un contenedor. Cuando se ejecuta el contenedor, imprime un mensaje de confirmación y termina.

## 2. Descargar e instalar lib0t
Descargar la imagen de lib0t de los repositorios de Docker
```bash
sudo docker pull bsjaramillo/lib0t
```
Verifica que la imagen se haya descargado
```bash
sudo docker images
```
# Crear una sala de chat
Con este servidor se da la posibilidad de poder crear multiples salas de chat en una misma máquina. Por lo cual es necesario crear un **workspace** o carpeta, que separe los archivos de configuración (templates, motd, scripts, etc) de cada sala de chat.
## 1.0 Crear el workspace para la sala de chat
Primero crear la siguiente carpeta
```bash
sudo mkdir ~/lib0t
```
Esta carpeta contendrá todos los workspaces de cada sala de chat.
Luego elegir el nombre del **workspace** para la sala de chat, elegir un nombre corto y evitar usar caracteres especiales (tildes, simbolos especiales, etc), como ejemplo se creará un **workspace** llamado room1.

*sudo mkdir ~/lib0t/**workspace***
```bash
sudo mkdir ~/lib0t/room1
```
## 2.0 Descargar archivos de configuración para la sala de chat
Para configurar una sala de chat (nombre, puerto, etc) en Windows se utilizaba la interfaz de usuario, en las distribuciones Linux por lo general no existe o no es posible realizarlo de la misma manera. Por lo cual es necesario descargar y editar unos archivos de configuración en formato JSON.
### 2.1 Crear la carpeta para los archivos de configuración

*sudo mkdir ~/lib0t/**workspace**/Settings*
```bash
sudo mkdir ~/lib0t/room1/Settings
```
### 2.1 Descargar los archivos de configuración
AppSettings.json, este archivo de configuración es necesario para la creación de la sala de chat, en este se establecen los valores o parámetros para el nombre de la sala, puerto, contraseña, habilitar para ib0t, etc.

*sudo wget -O ~/lib0t/**workspace**/Settings/AppSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/AppSettings.json*
```bash
sudo wget -O ~/lib0t/room1/Settings/AppSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/AppSettings.json
```
CommandsSettings, este archivo de configuración es necesario en tiempo de ejecución de la sala, no es necesario editar.

*sudo wget -O ~/lib0t/**workspace**/Settings/CommandsSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/CommandsSettings.json*
```bash
sudo wget -O ~/lib0t/room1/Settings/CommandsSettings.json https://raw.githubusercontent.com/bsjaramillo/lib0t/main/Settings/CommandsSettings.json

```
## 3.0 Configurar la sala de chat
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

*sudo nano ~/lib0t/**workspace**/Settings/AppSettings.json*
```bash
sudo nano ~/lib0t/room1/Settings/AppSettings.json
```

Para moverse dentro del archivo utilizar las flechas de dirección del teclado. Una vez terminado de editar el archivo guardar los cambios presionar las teclas "Ctrl+x", luego la tecla "y" y por último dar enter.
## 4.0 Crear la sala de chat
En este punto recordar el nombre del **workspace** elegido en el punto 1.0 y el **puerto** establecido en el archivo de configuración AppSettings.json. Ejecutar el siguiente comando.

*sudo docker run -d -it -v ~/lib0t/**workspace**:/lib0t/**workspace** --name **workspace** -p **puerto**:**puerto** bsjaramillo/lib0t **workspace***
```bash
sudo docker run -d -it -v ~/lib0t/room1:/lib0t/room1 --name room1 -p 54321:54321 bsjaramillo/lib0t room1
```
Para verificar que la sala ha sido creada con sin problemas, ejectuar el siguiente comando.

*sudo docker logs **workspace***
```bash
sudo docker logs room1
```
Al ejecutar este comando debería visualizar un mensaje diciendo que se ha creado la sala en el puerto establecido.

## 5.0 Crear las reglas en el Firewall
Al igual que en Windows es necesario crear reglas en el Firewall para permitir conexiones al servidor.

Para esto ejecutar los commandos

*sudo ufw allow **puerto**/tcp*

*sudo ufw allow **puerto**/udp*

```bash
sudo ufw allow 54321/tcp
sudo ufw allow 54321/udp
```

## 6.0 Iniciar, Apagar, Reinciar servidor (lib0t)

Para iniciar el servidor

*sudo docker start **workspace***
```bash
sudo docker start room1
```
Para apagar el servidor

*sudo docker stop **workspace***
```bash
sudo docker stop room1
```
Para reinciar el servidor, este comando se puede utilizar cuando se realiza un cambio en el archivo de configuración AppSettings.json y es necesario reinciar el servidor para aplicar los cambios.

*sudo docker restart **workspace***
```bash
sudo docker restart room1
```

# RabbitMQ example

commands for run RabbitMq from docker, see https://hub.docker.com/_/rabbitmq

docker run -d --hostname my-rabbit-host --name my-rabbit -e RABBITMQ_DEFAULT_USER=user -e RABBITMQ_DEFAULT_PASS=password rabbitmq:3-management
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

RabbitMQ itself must be running on localhost or you need to change the settings in appsettings.json
you can run both projects and send a test message to RabbitMQ from WebAppProducer
then the WebAppConsumer will accept and display the message in the Debug window

сам RabbitMQ должен быть запущен на localhost или нужно поменять настройки в appsettings.json 
можно запустить оба проекта и передать тестовое сообщение в RabbitMQ из WebAppProducer 
тогда WebAppConsumer примет и отобразит сообщение в Debug window

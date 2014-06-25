IAgenteSmsSharp
===============
[![Build Status](https://travis-ci.org/giacomelli/IAgenteSmsSharp.png?branch=master)](https://travis-ci.org/giacomelli/IAgenteSmsSharp)

Client library .NET para integração HTTP com o serviço de envio de SMS da IAgente (http://www.iagentesms.com.br)

--------

Características
===
 - Envio de SMS individual
 - Envio de SMS em lote
 - Consulta de status do SMS
 - Testes unitários
 - Testes funcionais 
 - Código 100% documentado
 - Validado com FxCop e StyleCop 
 
--------

Instalação
===
PM> Install-Package IAgenteSmsSharp


Usando
===

Criando o client
---
```csharp
var client = new SmsClient(
"http://www.iagentesms.com.br/webservices/http.php", 
"<username>", 
"<senha>");

```

Enviando mensagem
---
```csharp
var msg = new Message();

// Identificador da mensagem em seu sistema (codigosms).
msg.ExternalId = 1;

// Número de telefone com DDD.
msg.Receivers.Add("51999999999"); 
msg.Text = "Exemplo de envio de mensagem";

client.SendMessage(msg);

```

Consultando o status de uma mensagem
---
```csharp
// Informe o ExternalId utilizado no envio da mensagem.
MessageStatus status = client.GetMessageStatus(1); 

```

--------

FAQ
-------- 
Problemas? 
 - Dê uma olhada no arquivo [SmsClientTest](src/IAgenteSmsSharp.FunctionalTests/SmsClientTest.cs) do projeto de testes funcionais, ele é um ótimo exemplo de como utilizar a library (não esqueça de configurar o arquivo app.config).
 - Leia o [documento de integração http](docs/api-http-iagentesms.pdf) fornecido pela IAgenteSms.

Roadmap
-------- 
 - Implementar o ISmsClient.GetMessageCallback para interpretação do callback de atualização enviado pela IAgente.
 
--------

Como colaborar?
======

- Crie um fork [IAgenteSmsSharp](https://github.com/giacomelli/IAgenteSmsSharp/fork). 
- Você fez uma melhoria? [Faça um pull request](https://github.com/giacomelli/IAgenteSmsSharp/pull/new/master).


Licença
======

Licenciada sobre a licença MIT.
Em outras palarvas, você pode utilizar essa bibleoteca para desenvolver qualquer tipo de software: open source, comercial, proprietário e alienígena.


Change Log
======
 - 1.0.0 Primeira versão.

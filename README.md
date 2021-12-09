## Description

Purpose of this repository is to provide some sample code, which is supposed to be reviewed during an interview for a .NET developer position.
The provided solution implements the rate-limiting task described below, while having some possibly controversial decisions. It's required to make a code review like it's a production code ready to be merged, so approach this the same way you do it usually at work.

### Task

Rate limiting involves restricting the number of requests that can be made by a client. Each client is identified with an access token used for every request to a resource. To prevent abuse of the server, APIs enforce rate-limiting techniques.

Based on the client, the rate-limiting application can decide whether to allow the request to go through or not. The client makes an API call to a particular resource; the server checks whether the request for this client is within the limit. If the request is within the limit, then the request goes through, otherwise, the API call is restricted.

Some examples of request-limiting rules (you could imagine any others)
* X requests per timespan;
* a certain timespan passed since the last call;
* for US-based tokens, we use X requests per timespan, for EU-based - certain timespan passed since the last call.

The goal is to design a class(-es) that manage rate limits for every provided API resource by a set of provided *configurable and extendable* rules. For example, for one resource you could configure the limiter to use Rule A, for another one - Rule B, for a third one - both A + B, etc. Any combinations of rules are possible, keep this fact in mind when designing the classes.

Use simple in-memory data structures to store the data; don't rely on a particular database. Do not prepare any complex environment,
a class library with a set of tests is more than enough. Don't worry about the API itself, including auth token generation - there is no real API environment required.
For simplicity, you can implement the API resource as a simple C# method accepting a user token, and at the very beginning of the method, you set up your classes and ask whether further execution is allowed for this particular callee.

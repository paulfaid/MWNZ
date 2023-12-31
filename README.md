## Companies API for evaluation. 

A simple service to convert xml from a downstream service into json. Written to meet the requirements of [MWNZ evaluation](https://github.com/MiddlewareNewZealand/evaluation-instructions)

## Dependancies

 - ASP .Net 6
 - Node.js v18

## Getting started

After cloning the repo, execute `npm install`

The following commands are available in the package.js file

`npm run test.unit`

`npm run start`

`npm run test.integration`

## Things worth considering.

Service
 - Add a ping / health check endpoint. Requirements depend on operating env.
 - Address compiler warning in the unit test project, requires binding redirects.
 - Remove Swagger from the launchSettings.
 
Unit Tests
 - Switch to NUnit (or similar) to get richer asserts.
 - Increase coverage, ensure all xml parsing scenarios are covered.
 - Consider testing the calls between classes.

Integration Tests
 - Add an imposting service (Mountebank) so the integration tests talk to a mocked backend.
 - Consider switching to TypeScript for the integration tests.

CI/CD pipeline
 - GitAction or Azure Devops? I've not implemented these before. My experience has been with Jenkins on a private cloud.

Deployment
 - Docker / Kubernetes?
 - Implement ssl in an upstream container (nginx)

Safety considerations
 - XML sanitisation. We shouldn't trust the backend. DotNet 6 does not process DTDs by default.
 - Input sanitisation. The only input we accept is an integer in the path.
   
Error messages,  Exceptions and Logging.
 - Error messages: We need to ensure we do not leak info via error messages. Do not include unfiltered exception content in error messages.
 - Exception: don't lose any info contained in excpetion messages, what can't be returned to the user should be logged.
 - Logging: We need to log sufficient information to be able to investigate any faults that do occur.
    
---    
Out of curiosity I did a simpler implementation in Dart: [CompaniesApi Dart Implementation](https://github.com/paulfaid/MWNZ_Dart)





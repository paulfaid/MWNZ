const chai = require('chai');
const expect = chai.expect;
const chaiHttp = require('chai-http');
chai.use(chaiHttp);

  describe('companiesApi', function () {
    it('should return valid company data for id:1', function () {
        chai.request("http://localhost:5006")
        .get("/v1/companies/1")
        .end(function(err,resp){
            expect(err).to.be.equal(null, "Is the service up? Try 'npm run start' to start the service locally.");
            expect(resp).to.have.status(200);
            expect(resp).to.be.json;
            expect(resp.body).to.have.property('id').to.equal(1);
            expect(resp.body).to.have.property('name').to.equal('MWNZ');
            expect(resp.body).to.have.property('description').to.equal('..is awesome');
           
        });            
    });

    it('should return error for id:0', function () {
        chai.request("http://localhost:5006")
        .get("/v1/companies/0")
        .end(function(err,resp){
            expect(err).to.be.equal(null, "Is the service up? Try 'npm run start' to start the service locally.");
            expect(resp).to.have.status(404);
            expect(resp).to.be.json;
            expect(resp.body).to.have.property('error').to.equal('not_found');
            expect(resp.body).to.have.property('errorDescription').to.equal('The requested resource was not found on the backend service');
            
        });            
    });
});

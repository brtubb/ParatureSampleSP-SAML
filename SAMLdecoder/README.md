# Sample SAML SP usage with Microsoft Parature
This is a simple example of using [Kentor's open source SAML library](https://github.com/KentorIT/authservices) in a small web application. Comments point to configuration's required for production use. This is provided as-is for demonstration purposes, and should assist in any deep integrations with Parature that require security (Portal or Service Desk).

## Contribute
Please see the separate documentation on [contributing](CONTRIBUTING.md).

## Usage
This is a really basic site that can use a Parature Portal or Service Desk as a SAML2 Identity Provider (IdP). Steps for testing:

1. [Send a request](http://partners.support.parature.com/) for enabling a Single Sign On Endpoint. Specify:
 * Whether you have an existing Parature Deployment
 * Account and Department Ids (if you have an environment already)
 * Url and port of testing environment (localhost is perfectly fine to use)
 * If known - production URL of your service
2. We'll respond back with information you'll populate in the Web.Config:
 * kentor.authServices[entityId] - this is {YOUR-SP-NAME-IN-PING} mentioned in Global.asax
 * identityProviders.add[entityId] - our IdP's entity Id.

You should be able to run this example application and trace the SAMLP and retrieve the claims data. Status.aspx contains this whole process.
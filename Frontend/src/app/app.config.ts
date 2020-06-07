const {
  CLIENT_ID,
  ISSUER,
  REDIRECT_URI,
  OKTA_TESTING_DISABLEHTTPSCHECK
} = process.env;

export default {
  oidc: {
    clientId: `${CLIENT_ID}`,
    issuer: `${ISSUER}`,
    redirectUri: `${REDIRECT_URI}`,
    scopes: ['openid', 'profile', 'email'],
    testing: {
      disableHttpsCheck: `${OKTA_TESTING_DISABLEHTTPSCHECK}`
    }
  },
  resourceServer: {
    baseApiUrl: 'https://localhost:5001/api/v1'
  },
  userIdClaim: 'uid'
};

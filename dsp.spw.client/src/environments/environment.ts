export const environment = {
  production: false,
  microsoft: {
    graph: 'https://graph.microsoft.com/v1.0',
    entraId: {
      tenantId: '72e42a61-9cee-4b78-8828-29b226163bd7',
      clientId: '95e3fcdb-5f1c-4a70-ae15-56e68a9337ed',
      redirectUri: 'http://localhost:4200/',
      exposedApis: ['api://95e3fcdb-5f1c-4a70-ae15-56e68a9337ed/api.scope'],
      scopes: ['user.read'],
    },
  },
  api: 'https://localhost:7239',
};

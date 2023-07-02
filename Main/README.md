to deploy, first create a service principle with and setup following env var, this allows terraform to gain access to
azure

ARM_SUBSCRIPTION_ID="<azure_subscription_id>"  
ARM_TENANT_ID="<azure_subscription_tenant_id>"  
ARM_CLIENT_ID="<service_principal_appid>"  
ARM_CLIENT_SECRET="<service_principal_password>"

once infra is ready, you need to grab the publishing profile from webapp and put it in github secrets as
AZURE_WEBAPP_PUBLISH_PROFILE

deployment should be done automatically on push to master branch

terraform {
    required_providers {
        azurerm = {
            source = "hashicorp/azurerm"
        }
    }
}

provider "azurerm" {
    features {}
}

resource "azurerm_resource_group" "resource_group" {
    name     = "cloud-resume-challenge-rg"
    location = "westus"
}

resource "azurerm_service_plan" "service_plan" {
    name                = "cloud-resume-challenge-sp"
    resource_group_name = azurerm_resource_group.resource_group.name
    location            = azurerm_resource_group.resource_group.location
    os_type             = "Linux"
    sku_name            = "F1"
}


variable "pg_username" {
    type = string
}
variable "pg_password" {
    type = string
}
resource "azurerm_postgresql_flexible_server" "postgres" {
    name                = "cloud-resume-challenge-postgres"
    location            = "westus"
    resource_group_name = azurerm_resource_group.resource_group.name
    sku_name            = "B_Standard_B1ms"

    administrator_login    = var.pg_username
    administrator_password = var.pg_password
    version                = "14"

    storage_mb = 32768
}

// allow all azure resource to access the postgres server
resource "azurerm_postgresql_flexible_server_firewall_rule" "firewall_rule_all_azure" {
    name             = "example-fw"
    server_id        = azurerm_postgresql_flexible_server.postgres.id
    start_ip_address = "0.0.0.0"
    end_ip_address   = "0.0.0.0"
}

resource "azurerm_linux_web_app" "web-app" {
    name                = "cloud-resume-challenge-webapp"
    resource_group_name = azurerm_resource_group.resource_group.name
    location            = azurerm_service_plan.service_plan.location
    service_plan_id     = azurerm_service_plan.service_plan.id

    site_config {
        application_stack {
            dotnet_version = "7.0"
        }
        always_on = false
    }

    connection_string {
        name  = "DefaultConnection"
        type  = "Custom"
        value = "Host=${azurerm_postgresql_flexible_server.postgres.fqdn};Port=5432;Database=postgres;Username=${azurerm_postgresql_flexible_server.postgres.administrator_login};Password=${azurerm_postgresql_flexible_server.postgres.administrator_password}"
    }
}

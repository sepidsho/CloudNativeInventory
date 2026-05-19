# Cloud Native Inventory API

Detta projekt är en containerbaserad mikrotjänst byggd i .NET 9 för hantering av ett produktlager. Applikationen är distribuerad på Azure med fokus på säkerhet, automatisering och Cloud Native-best-practices.

## Azure-tjänster som används
* **Azure Container Apps:** Används för att köra API:t i en serverlös, skalbar miljö.
* **Azure Container Registry (ACR):** Sparar och hanterar applikationens Docker-images.
* **Azure Key Vault:** Hanterar applikationens hemligheter och känslig konfiguration på ett säkert sätt.
* **Managed Identity:** Används för lösenordslös och säker autentisering mellan Azure Container App och Azure Key Vault.

---

## Köra API:t lokalt
För att köra applikationen lokalt utan att riskera att checka in några hemligheter används en lokal **In-Memory-databas**.

1. Klona repot:
   ```bash
   git clone <DIn-REPO-URL>
   
   2.Gå till projektmappen:
   cd CloudNativeInventory.Api

   3.Starta applikationen:
   dotnet run

   Applikationen seedar automatiskt lite testdata vid start.
   

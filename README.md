# VideoKatalog


---

## Povla?enje projekta sa GitHub-a

U terminalu izvr�iti:

```bash
git clone https://github.com/USERNAME/REPO_NAME.git
cd VideoKatalog
```

> Zameniti `USERNAME/REPO_NAME` stvarnim imenom repozitorijuma.

---

## Provera NuGet paketa i putanja do baze

Pre nego �to pokrene� projekat, proveri da li su instalirani svi potrebni NuGet paketi:

```xml
<PackageReference Include="Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore" Version="8.0.23" />
<PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.23" />
<PackageReference Include="Microsoft.AspNetCore.Identity.UI" Version="8.0.23" />
<PackageReference Include="Microsoft.DotNet.Scaffolding.Shared" Version="8.0.23" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.23" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.23" />
```

U **Visual Studio-u**:

* Desni klik na projekat ? **Manage NuGet Packages**
* Proveriti da li su svi paketi instalirani i da verzije odgovaraju

Proveriti da je **connection string** u `appsettings.json` ispravan, npr:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=VideoKatalogDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> Ako koristi� SQL Server sa drugim imenom instance, prilagodi `Server=` deo.

---

## Entity Framework migracije (OBAVEZNO)

U **Visual Studio ? Tools ? NuGet Package Manager ? Package Manager Console**:

```powershell
Add-Migration InitialCreate
Update-Database
```

Ovim se:

* Kreira struktura baze
* Povezuje aplikaciju sa `VideoKatalogDb`

---

## Pode�avanje baze podataka (SQL Server)

Aplikacija koristi bazu podataka `VideoKatalogDb`.

### Pokretanje SQL skripte

U **SQL Server Management Studio (SSMS)**:

1. Povezati se na SQL Server instancu
2. Otvoriti skriptu `VideoKatalogDb.sql`
3. Izvr�iti skriptu da se kreiraju i popune nu�ne tabele

---

## Automatski kreirani korisnici

Prilikom **prvog pokretanja aplikacije**, generi�u se:

| Rola  | Email            | Password  |
| ----- | -----------------| --------- |
| Admin | [admin@gmail.com]| Admin123! |
| Moderator | [moderator@gmail.com]| Moderator123! |
| User  | [user@gmail.com] | User123!  |

> Slu�e za testiranje i prijavljivanje u aplikaciju.

---

##  Pokretanje aplikacije

* Klikom na **Run** u Visual Studio-u


> Proveriti da je SQL Server pokrenut pre startovanja aplikacije.

---

## Napomene

* Migracije se pokre?u samo prvi put ili kada se model menja
* SQL Server mora biti aktivan pre startovanja aplikacije
* Connection string u `appsettings.json` mora odgovarati lokalnoj instanci SQL Server-a

---

## Tehnologije

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* Razor Pages

---

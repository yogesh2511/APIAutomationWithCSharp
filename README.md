# **Trello API Automation with RestSharp & C# (Page Object Model)**  

This project demonstrates **API automation for Trello** using **RestSharp** in **C#**, following the **Page Object Model (POM)** design pattern for better maintainability and reusability.  

---

## **📌 Table of Contents**  
1. [Project Overview](#-project-overview)  
2. [Prerequisites](#-prerequisites)  
3. [Setup & Configuration](#-setup--configuration)  
4. [Project Structure](#-project-structure)  
5. [Running Tests](#-running-tests)  
6. [GitHub Push Protection](#-github-push-protection)  
7. [Best Practices](#-best-practices)  

---

## **🚀 Project Overview**  
- **Tech Stack**:  
  - **C#** (.NET 6+)  
  - **RestSharp** (API client)  
  - **NUnit** (Testing framework)  
  - **Page Object Model (POM)** (Design pattern)  
- **Key Features**:  
  - ✅ Trello **Board**, **List**, and **Card** management  
  - ✅ **Environment-based configurations** (no hardcoded secrets)  
  - ✅ **Reusable API request methods**  
  - ✅ **GitHub secret scanning protection**  

---

## **🔧 Prerequisites**  
- [.NET 6+ SDK](https://dotnet.microsoft.com/download)  
- [Visual Studio / VS Code](https://visualstudio.microsoft.com/)  
- [Trello API Key & Token](https://trello.com/power-ups/admin)  
- [Git](https://git-scm.com/)  

---

## **⚙️ Setup & Configuration**  

### **1. Clone the Repository**  
```bash
git clone https://github.com/yogesh2511/APIAutomationWithCSharp.git
cd trello-api-automation
```

### **2. Configure Trello API Keys**  
Instead of hardcoding secrets, use **environment variables**:  
```bash
# PowerShell (Windows)
$env:TRELLO_API_KEY = "your_api_key"
$env:TRELLO_API_TOKEN = "your_api_token"

# Bash (Linux/macOS)
export TRELLO_API_KEY="your_api_key"
export TRELLO_API_TOKEN="your_api_token"
```

### **3. Install Dependencies**  
```bash
dotnet restore
```

---

## **📂 Project Structure (Page Object Model)**  
```plaintext
TrelloAPIAutomation/
├── 📁 Models/                # Data models (Board, List, Card)
├── 📁 Pages/                 # Page Objects (API endpoints)
│   ├── BoardPage.cs         # Board-related API methods
│   ├── ListPage.cs          # List-related API methods
│   └── CardPage.cs          # Card-related API methods
├── 📁 Tests/                 # NUnit test cases
│   ├── BoardTests.cs        
│   ├── ListTests.cs         
│   └── CardTests.cs         
├── 📁 Utils/                 # Helpers & Configs
│   ├── ApiClient.cs         # RestSharp client setup
│   └── Config.cs            # Environment config loader
├── .gitignore               # Excludes secrets & .vs/
└── README.md                # This file
```

---

## **▶️ Running Tests**  
Run all tests via:  
```bash
dotnet test
```

### **Example Test (Board Creation)**  
```csharp
[Test]
public void CreateBoard_ShouldReturnSuccess()
{
    var boardPage = new BoardPage();
    var response = boardPage.CreateBoard("My New Board");
    
    Assert.AreEqual(200, (int)response.StatusCode);
    Assert.IsNotNull(response.Data.Id);
}
```

---

## **🔒 GitHub Push Protection**  
This repo has **secret scanning** enabled. To avoid push rejections:  
1. **Never commit `.vs/`, `appsettings.json`, or secrets**  
2. **Use `git filter-repo` if secrets were accidentally committed**  
   ```bash
   git filter-repo --path .vs/ --invert-paths --force
   git push origin --force
   ```

---

## **✅ Best Practices**  
✔ **Use `dotnet user-secrets` for local dev**  
✔ **Follow POM for maintainable API tests**  
✔ **Add `.vs/` and `bin/` to `.gitignore`**  
✔ **Rotate API keys if exposed**  

---

## **📜 License**  
MIT © Yogesh Solanki

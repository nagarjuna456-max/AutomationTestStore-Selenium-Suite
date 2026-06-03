# Automation Test Store - UI Test Automation Suite

This repository contains a production-grade automated UI testing framework built to validate core consumer flows on the **Automation Test Store** web platform. [cite_start]The suite is engineered using **C#**, **Selenium WebDriver**, and **NUnit**[cite: 6, 7].

---

## 🛠️ Technology Stack & Dependencies

* [cite_start]**Language Runtime:** .NET SDK (v10.0.300) [cite: 7]
* [cite_start]**UI Automation Framework:** Selenium WebDriver C# (v4.44.0) [cite: 7]
* [cite_start]**Test Engine Framework:** NUnit (v4.3.2) [cite: 7]
* **Test Runner Integration:** NUnit3TestAdapter (v5.0.0)
* **Test Execution SDK:** Microsoft.NET.Test.Sdk (v17.14.0)

---

## 📦 Project Architecture Highlights

* [cite_start]**Explicit Synchronization Loops:** Implements robust asynchronous poll configurations via `WebDriverWait` handlers to handle modern AJAX page transitions seamlessly[cite: 20].
* [cite_start]**Dynamic Test Data Partitioning:** Features programmatic GUID string generators (`Guid.NewGuid()`) within the Registration module to guarantee completely independent, collision-free, and repeatable automation runs[cite: 20].
* **Robust Semantic Selectors:** Utilizes flexible locator expressions (such as partial text containment matching via XPath) to protect tests against minor presentation layer or layout updates.

---

## 🚀 Getting Started (Local Setup)

### Prerequisites
Ensure you have the following installed on your machine:
* [cite_start][.NET SDK 10.0](https://dotnet.microsoft.com/download) [cite: 7]
* [Visual Studio 2022](https://visualstudio.microsoft.com/)
* Google Chrome Browser

### Installation & Build
1. Clone this repository to your local machine:
   ```bash
   git clone https://github.com/nagarjuna456-max/AutomationTestStore-Selenium-Suite
# MVC_project
## 資工系學生資料網頁
* 進入學生管理介面
* 登入學生資料
* 顯示比賽資訊
* 呈現學生選課狀況
  P.S.輸入選課代碼及成績
## 使用方法
1. [複製檔案到本地](https://github.com/Freedom-Hank/MVC_project.git)
2. 設置資料庫
- 新增 `appsettings.Development.json`，設置連接字串來連接您的 SQL Server
```json=
{
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-StudentManagement-f606416b-0ba3-4447-9a03-80f84a360bcf;Trusted_Connection=True;MultipleActiveResultSets=true"
    },
    "Logging": {
      "LogLevel": { 
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    }
}
 ```
3. 建立資料庫
- 在 Visual Studio 中，打開 Package Manager Console，執行以下命令來建立資料庫：
```bash
Add-Migration InitialCreate
Update-Database
```

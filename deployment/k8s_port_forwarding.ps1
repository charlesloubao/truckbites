Write-Host "1.Waiting 10 seconds for pods to initialize..." -ForegroundColor Yellow;
Start-Sleep -Seconds 10;
Write-Host "2. Launching Port Forwards in new windows..." -ForegroundColor Green;
Start-Process powershell -ArgumentList "-NoExit", "-Command kubectl port-forward svc/customer-webapp 8080:8080";
Start-Process powershell -ArgumentList "-NoExit", "-Command kubectl port-forward svc/vendor-webapp 8081:8081";
Start-Process powershell -ArgumentList "-NoExit", "-Command kubectl port-forward svc/phpmyadmin 8082:8082";
Write-Host "Done!" -ForegroundColor Green
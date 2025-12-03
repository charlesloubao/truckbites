Write-Host "1. Building Docker Images..." -ForegroundColor Cyan; `
docker build -t truckbites-customer-webapi:latest -f ./Eleven95.TruckBites.WebApi/Dockerfile .; `
docker build -t truckbites-vendor-webapi:latest -f ./Eleven95.TruckBites.Vendor.WebApi/Dockerfile .; `
docker build -t truckbites-customer-webapp:latest -f ./Eleven95.TruckBites.WebApp/Eleven95.TruckBites.WebApp/Dockerfile .; `
docker build -t truckbites-vendor-webapp:latest -f ./Eleven95.TruckBites.Vendor.WebApp/Eleven95.TruckBites.Vendor.WebApp/Dockerfile .; `
docker build -t truckbites-ef-migrations:latest -f ./Eleven95.TruckBites.EF/migrations.Dockerfile .; `
Write-Host "2. Copying images to Minikube (This may take a moment)..." -ForegroundColor Yellow; `
minikube image load truckbites-customer-webapi:latest; `
minikube image load truckbites-vendor-webapi:latest; `
minikube image load truckbites-customer-webapp:latest; `
minikube image load truckbites-vendor-webapp:latest; `
minikube image load truckbites-ef-migrations:latest; `
Write-Host "3. Applying Kubernetes Configuration..." -ForegroundColor Cyan; `
kubectl apply -f deployment/2_deploy.yml; `
Write-Host "4. Restarting Pods to load new images..." -ForegroundColor Cyan; `
kubectl rollout restart deployment; `
Write-Host "Done!" -ForegroundColor Green
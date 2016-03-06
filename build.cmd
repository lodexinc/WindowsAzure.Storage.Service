dnu restore
echo "Start build"
dnu pack src\WindowsAzure.Storage.Service --configuration Release
echo "End build"
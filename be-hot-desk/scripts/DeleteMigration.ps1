$path = $MyInvocation.MyCommand.Path
$parentPath = $(Split-Path (Split-Path $path -Parent) -Parent)
dotnet ef migrations remove --project $parentPath\src\HotDesk.Api --startup-project $parentPath\src\HotDesk.Api --context HotDeskDbContext
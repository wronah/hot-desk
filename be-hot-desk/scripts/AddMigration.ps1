$path = $MyInvocation.MyCommand.Path
$parentPath = $(Split-Path (Split-Path $path -Parent) -Parent)
$message = $(Read-Host -Prompt 'Input message')

dotnet ef migrations add $message --project $parentPath\src\HotDesk.Api --startup-project $parentPath\src\HotDesk.Api --output-dir $parentPath\src\HotDesk.Api\Persistence\HotDesk\Migrations --context HotDeskDbContext

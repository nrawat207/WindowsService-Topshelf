param($rootPath)

if (!$rootPath){
	$rootPath = resolve-path .\
}
& "$rootPath\WindowsServiceTopshelf.Exe" /uninstall /serviceName:"WindowsServiceTopshelf"
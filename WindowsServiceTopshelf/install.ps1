param($rootPath)

if (!$rootPath){
	$rootPath = resolve-path .\
}
& "$rootPath\WindowsServiceTopshelf.exe" install
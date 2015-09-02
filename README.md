Unity3DGameResource
===================

unity3d 资源加载 API
===================================
	这里将所有资源加载方式都进行了API封装。

### 阻塞加载
	1.Resource.Load
	2.AssetBundle.CreateFromFile + AssetBundle.Load
	3.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load

### 非阻塞加载
	1.WWW + AssetBundle.Load
	2.WWW + AssetBundle.LoadAsync
	3.AssetBundle.CreateFromFile + AssetBundle.LoadAsync
	4.File Read all + AssetBundle.CreateFromMemory + AssetBundle.Load
	5.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.LoadAsync
	6.File Read async + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load
	7.File Read async + AssetBundle.CreateFromMemory + AssetBundle.Load
	8.File Read async + AssetBundle.CreateFromMemory + AssetBundle.LoadAsync

### 开发模式
	对在编辑器模式下进行了额外的封装。使得编辑器模式下读取的资源用prefab替换AssetBundle资源。


关于unity3d的技术文章请前往 http://www.luzexi.com

Unity3DGameResource
===================

unity3d 资源加载 API
===================================
  这里将所有资源加载方式都进行了API封装。
  This plugin inclue all of the resources loading method.

### 阻塞加载(sync load)
	1.Resource.Load
	2.AssetBundle.CreateFromFile + AssetBundle.Load
	3.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load

### 非阻塞加载(async load)
	1.WWW + AssetBundle.Load
	2.WWW + AssetBundle.LoadAsync
	3.AssetBundle.CreateFromFile + AssetBundle.LoadAsync
	4.File Read all + AssetBundle.CreateFromMemory + AssetBundle.Load
	5.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.LoadAsync
	6.File Read async + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load
	7.File Read async + AssetBundle.CreateFromMemory + AssetBundle.Load
	8.File Read async + AssetBundle.CreateFromMemory + AssetBundle.LoadAsync

### 开发模式(develop state)
  对在编辑器模式下进行了额外的封装。使得编辑器模式下读取的资源用prefab替换AssetBundle资源。
  When you are in the editor , it will load prefab instead by assetbundle.


### 加密(encrypt)
  你可以选择加解密方式
  You can choose the encrypt type.

### 接口描述 (interface description)
    1.WWW load function.
    2.File load function. (include sync load and async load)
    3.AssetBundle load function. (include CreateFromFile , Load , LoadAsnyc, CreateFromMemory , and CreateFromMemoryImmediate)
    4.Resources stay load function. (the resources will storage in the memory if you not clear it)
    5.Resources no stay load function. (the resources will not storage in the memory)
    6.Resources load by unity3d api. (resources is load by Resources.Load)

### 未完成 ， 未测试(Sorry , it is not complete)


关于unity3d的技术文章请前往 http://www.luzexi.com

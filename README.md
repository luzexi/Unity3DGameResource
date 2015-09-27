Unity3DGameResource
===================

This is a Unity3d assetbundle API. (这里有unity3d加载assetbundle的api)

This plugin inclue all of the assetbundle loading method.(这里将所有Assetbundle加载方式都进行了API封装)

Email : jesse_luzexi@163.com

Blog: http://www.luzexi.com (in chinese)
  

### Load sync (阻塞加载)
	1.Resource.Load
	2.AssetBundle.CreateFromFile + AssetBundle.Load
	3.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load

### Load async (非阻塞加载)
	1.WWW + AssetBundle.Load
	2.WWW + AssetBundle.LoadAsync
	3.AssetBundle.CreateFromFile + AssetBundle.LoadAsync
	4.File Read all + AssetBundle.CreateFromMemory + AssetBundle.Load
	5.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.LoadAsync
	6.File Read async + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load
	7.File Read async + AssetBundle.CreateFromMemory + AssetBundle.Load
	8.File Read async + AssetBundle.CreateFromMemory + AssetBundle.LoadAsync

### Interface description (接口描述)
    1.WWW load function.
    2.File load function. (include sync load and async load)
    3.AssetBundle load function. (include CreateFromFile , Load , LoadAsnyc, CreateFromMemory , and CreateFromMemoryImmediate)
    4.Resources stay load function. (the resources will storage in the memory if you not clear it)
    5.Resources no stay load function. (the resources will not storage in the memory)
    6.Resources load by unity3d api. (resources is load by Resources.Load)
    7.ZipManager uncompress the zip file.

### Example (示例)
All of the example in the example folder. (所有示例都在example文件夹里)

1.example1 : How to use AbRequest to load amount of assetbundle. (如何使用AbRequest加载assetbundle)

2.example2 : How to use AbMgr to load amount of assetbundle. The difference between AbRequest and AbMrg is AbRequest will destroy the assetbundle when finished , but AbMrg not. (如何使用AbMgr加载assetbundle。与AbRequest不同的是，AbRequest在加载完毕后会销毁Assetbundle，AbMgr不会)

3.example3 : How to use AssetBundleLoader.CreateFromFile load assetbundle. It's synchronize. (如何使用AssetBundleLoader.CreateFromFile加载assetbundle，这是阻塞加载)

4.example4 : How to use FileLoader.ReadAllBytes + AssetBundleLoader.CreateFromMemoryImmediate to load assetbundle. (如何使用FileLoader.ReadAllBytes读取文件并用AssetBundleLoader.CreateFromMemoryImmediate加载2进制成为assetbundle)

5.example5 : How to use AssetBundleLoader.LoadBinary to load assetbundle by byte[] in async. (如何使用AssetBundleLoader.LoadBinary异步加载byte流成为assetbundle)

6.example6 : How to use AsyncLoader.StartLoad to load an Object from assetbundle. (如何使用AsyncLoader.StartLoad从assetbundle中加载某物体，此为异步)

7.example7 : How to use FileLoader.AsyncReadFile to read file and when it finished use AssetBundleLoader.CreateFromMemoryImmediate to load assetbundle. (如何通过FileLoader.AsyncReadFile 异步读取文件并使用AssetBundleLoader.CreateFromMemoryImmediate 加载bye流成为assetbundle)

8.example8 : How to use ZipManager to uncompress a zip file then use AssetBundleLoader.CreateFromFile to load assetbundle. (如何将多个assetbundle打成zip的文件进行解压，并在解压后对其进行加载并呈现画面)

### TODO
1.Assetbundle encrypt. (assetbundle 加密)

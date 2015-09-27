Unity3DGameResource
===================

This is a Unity3d assetbundle API. (这里有unity3d加载assetbundle的api)

This plugin inclue all of the assetbundle loading method.(这里将所有Assetbundle加载方式都进行了API封装)

Email : jesse_luzexi@163.com

Blog: http://www.luzexi.com (in chinese)
  

### sync load (阻塞加载)
	1.Resource.Load
	2.AssetBundle.CreateFromFile + AssetBundle.Load
	3.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load

### async load (非阻塞加载)
	1.WWW + AssetBundle.Load
	2.WWW + AssetBundle.LoadAsync
	3.AssetBundle.CreateFromFile + AssetBundle.LoadAsync
	4.File Read all + AssetBundle.CreateFromMemory + AssetBundle.Load
	5.File Read all + AssetBundle.CreateFromMemoryImmediate + AssetBundle.LoadAsync
	6.File Read async + AssetBundle.CreateFromMemoryImmediate + AssetBundle.Load
	7.File Read async + AssetBundle.CreateFromMemory + AssetBundle.Load
	8.File Read async + AssetBundle.CreateFromMemory + AssetBundle.LoadAsync

### interface description (接口描述)
    1.WWW load function.
    2.File load function. (include sync load and async load)
    3.AssetBundle load function. (include CreateFromFile , Load , LoadAsnyc, CreateFromMemory , and CreateFromMemoryImmediate)
    4.Resources stay load function. (the resources will storage in the memory if you not clear it)
    5.Resources no stay load function. (the resources will not storage in the memory)
    6.Resources load by unity3d api. (resources is load by Resources.Load)

### Example (示例)
All of the example in the example folder.

1.example1 : How to use AbRequest to load amount of assetbundle.

2.example2 : How to use AbMgr to load amount of assetbundle. The difference between AbRequest and AbMrg is AbRequest will destroy the assetbundle when finished , but AbMrg not.

3.example3 : How to use AssetBundleLoader.CreateFromFile load assetbundle. It's synchronize.

4.example4 : How to use FileLoader.ReadAllBytes + AssetBundleLoader.CreateFromMemoryImmediate to load assetbundle.

5.example5 : How to use AssetBundleLoader.LoadBinary to load assetbundle by byte[] in async.

6.example6 : How to use AsyncLoader.StartLoad to load an Object from assetbundle.

7.example7 : How to use FileLoader.AsyncReadFile to read file and when it finished use AssetBundleLoader.CreateFromMemoryImmediate to load assetbundle.

8.example8 : How to use ZipManager to uncompress a zip file then use AssetBundleLoader.CreateFromFile to load assetbundle.

### TODO
1.Assetbundel encrypt.

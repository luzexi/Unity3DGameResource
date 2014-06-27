Unity3DGameResource
===================

封装unity3d的资源加载。
将几种资源加载接口封装起来，包括www,resouces,editor。
目的是让资源加载与释放便得更加简单，缩短开发时间。
封装还为切换本地与外部资源做了切换接口，方便在还没打包资源前进行调试工作。
www部分分为：AssetBundle的unity的cache和自行的cache，无cache方式，以及AssetBundle的异步资源加载几种。
resources：是unity3d本身的本地资源部分。
editor:是为了方便在编辑状态调用以上两种意外的editor状态下的资源。


关于unity3d的技术文章请前往 http://www.luzexi.com

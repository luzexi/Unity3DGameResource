Unity3DGameResource
===================
<br>
关于unity3d的动态资源文章请前往 http://www.luzexi.com/unity3d%E4%B9%8B%E5%A6%82%E4%BD%95%E5%B0%86%E5%8C%85%E5%A4%A7%E5%B0%8F%E5%87%8F%E5%B0%91%E5%88%B0%E6%9E%81%E8%87%B4/<br>
封装unity3d的资源加载。<br>
将几种资源加载接口封装起来，包括www,resouces,editor。<br>
目的是让资源加载与释放便得更加简单，缩短开发时间。<br>
封装还为切换本地与外部资源做了切换接口，方便在还没打包资源前进行调试工作。<br>
www部分分为：AssetBundle的unity的cache和自行的cache，无cache方式，以及AssetBundle的异步资源加载几种。<br>
resources：是unity3d本身的本地资源部分。<br>
editor:是为了方便在编辑状态调用以上两种意外的editor状态下的资源。<br>
<br>
<br>
关于unity3d的技术文章请前往 http://www.luzexi.com

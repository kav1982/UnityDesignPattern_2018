# 优化模式

> 优化模式中包含了四种不同的模式，这里实现了对象池和空间分区，四种模式的说明详见笔记部分

## 对象池 项目说明

Game模式下，点击鼠标可以设置多个目标点，Player（方块）会依次移向目标点；
- 层级列表中会显示对象池中的所有目标点，若未激活列表不为空，则先使用未激活的目标点
- 若未激活列表为空，则实例化目标点并加入对象池

### ObjPool

对象池类，包含了已激活列表和未激活列表，可用方法：
- AddObj：添加对象
- GetObj：获取对象，若无法获取则返回null
- DisableObj：用以替代销毁
- RemoveObj：彻底删除单个对象
- CleanPool：清空对象池

## 空间分区 项目说明

随机生成500个点，临近点之间可以连线

![](https://github.com/TYJia/GameDesignPattern_U3D_Version/blob/master/Assets/010OptimizationPatterns/SpatialPartition/Pics/PointLine.png)

Game模式下，点选OctTreeManager

- LineGenerator
  - 勾选UseOctTree，使用空间八叉树计算距离，反之用for循环500*500计算距离
    - 静态模式下，使用八叉树计算FPS能达到 86
    - 普通for循环，FPS只有 26

    ![](https://github.com/TYJia/GameDesignPattern_U3D_Version/blob/master/Assets/010OptimizationPatterns/SpatialPartition/Pics/FPS.png)
  - 勾选Animated，点会发生移动，这时会动态更新八叉树内容，帧率比静态降低，但同样高于普通for循环
- OctTree
  - 勾选Show，则显示八叉树，反之隐藏 

  ![](https://github.com/TYJia/GameDesignPattern_U3D_Version/blob/master/Assets/010OptimizationPatterns/SpatialPartition/Pics/OctTree.png)

### OtcTree

八叉树类
- 属性
  - MaxNum，单个节点最多能承受的点数
  - Show，显示八叉树
- 公开方法
  - ShowBox，显示八叉树
  - GenerateTree，生成八叉树
  - FindCloset，寻找最近的点
  - UpdateTree，更新八叉树内容

### LineGenerator

生成随机点，并在相近点之间连线
- CommonMethod，普通for循环计算点之间的距离，复杂度*O(n²)* 
- OctTreeMethod，八叉树计算距离，复杂度下降到*O(n)* 

> 以上仅为演示用，所以并没有优化八叉树

## [笔记](https://gpp.tkchu.me/optimization-patterns.html)

### 是什么、为什么（个人理解）

包含了

- 数据局部性
  - CPU缓存读写速度大于内存读写速度，所以要尽量减少缓存不命中（CPU从内存读取信息）的次数
  - 用连续队列代替指针的不断跳转
  - 不过此模式会让代码更复杂，并伤害其灵活性
- 脏标识模式
  - 需要结果时才去执行工作——避免不必要的计算或传输开销
  - 一种是被动状态变化时才计算，否则使用缓存；另一种是主动变化标识，否则不执行（例如存盘）
- 对象池模式
  - 对象池就像一包不同颜色的水彩笔，当我们使用时就拿出来，不用时就放回去——而不是使用时就买一只，不用时就扔进垃圾桶
  - 可以减少内存碎片，减少实例化与回收对象所面临的开销
- 空间分区
  - 建立细分空间用于存储数据（对象），可以帮助告诉定位对象，降低算法复杂度
  - 例如邮局寄信，如果只按身份证号邮寄，那就麻烦了，每封信平均要拿给几亿人确认是否是ta的；但是按空间分区后，就简单了——省份、城市、街道、小区、楼栋、单元、房号，于是很快就能定位到个人。

### 怎么做（对象池）

用对象池对之前实现的[例子](https://github.com/TYJia/GameDesignPattern_U3D_Version/tree/master/Assets/009DecouplingPatterns)做了优化：

- 之前每次点击鼠标会生成一个目标点，Player到达目标点后会将目标点回收（Destroy）
- 优化后点击鼠标，先会尝试从对象池“未激活列表”获取对象，无法获取才会生成新对象并放入对象池中的“已激活列表”；Player到达目标点后，会把对象从已激活列表放入未激活列表，并执行SetActive(false)方法

### 怎么做（空间分区）

- 这里我实现了一个八叉树简单示例，用来寻找最近的点

- 建立

  - 先寻找空间边界，建立父节点长方体
  - 若父节点中点数超过阈值，则分割成八个子节点长方体

- 寻找最近的点

  - 在点所在的和临近的立方体中寻找最近的点

  > 因为只是示例，所以并未完善临近立方体的查找，目前只用了八叉树结构临近的立方体，而非空间临近，有兴趣的同学可以进一步优化

- 更新点

  - 先看点是否在之前的长方体里，如果不在，则从当前节点移除，并查询是否在父节点里
  - 如果在父节点里，则向下查询在哪一个子节点里
  > 此示例只能更新点的位置，也就是八叉树中的内容，不能更新八叉树的结构，大家可以自行思考如何更新结构

#### 具体实现：

https://github.com/TYJia/GameDesignPattern_U3D_Version/tree/master/Assets/010OptimizationPatterns
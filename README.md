# PlantsVSZombies
 一个类似植物大战僵尸的游戏Demo
## 开发日志
### 2022.3.23
搭建了基础场景以及植物卡片面板的UI，通过定义状态实现了阳光的相关功能，即天空中阳光的生成与掉落、鼠标点击阳光的回收、向日葵生成太阳。创建了基础的网格地形。
### 2022.3.25
完善了植物卡片UI的功能，通过定义状态，实现卡片冷却中、缺少阳光和等待种植的逻辑和功能，点击卡片植物可在指定网格放置植物。
### 2022.3.28
完善卡片面板，改善向日葵生成阳光以及阳光拾取的视觉效果。对部分代码进行了重构，使逻辑更清晰。添加了植物豌豆射手。
### 2022.3.30
改善种植植物时的指针效果。添加了僵尸，通过定义状态实现了僵尸走路、攻击和死亡的功能。利用触发器实现植物和僵尸的交互。
### 2022.4.1
添加和改善僵尸各种状态的动画效果，使得僵尸行动更为真实。修复了一系列bug。添加了僵尸管理器，便于管理僵尸。
### 2022.4.2
完善了豌豆射手的行为逻辑，通过定义状态实现射击并使子弹与僵尸交互。使用对象池、继承的方式整理重构了整体的项目代码。建立了开始界面和动画。
### 2022.4.3
添加了开始、暂停、死亡、胜利的GUI。添加了路障僵尸并配置了僵尸的游戏生成。添加了小推车和铲子。改善并修复了一系列bug。
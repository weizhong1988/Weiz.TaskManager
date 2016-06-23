# Weiz.TaskManager
任务管理平台
1.	系统简介
Quartz.net是一个 开源的作业调度工具，相当于数据库中的 Job、Windows 的计划任务、Unix/Linux 下的 Cron，但 Quartz 可以把排程控制的更精细，对任务调度的领域问题进行了高度的抽象，实现作业的灵活调度。

本系统通过window服务来集成Quartz.net,通过修改配置文件和添加相应Job即可完成作业添加，使用简单方便。

2.	项目结构
系统目前包含八个项目组成：

Weiz.TaskManager.HouTai_New					1. 任务后台管理系统，任务，。
Weiz.TaskManager.Tasks						2. 所有任务的集合，目前只有一个TestJob。
Weiz.TaskManager.ServiceBus					3. 集成Quartz.netwindow服务，通过window服务来承载调度Weiz.TaskManager.Tasks 的各个任务Job。
Weiz.TaskManager.TaskUtility				4. 操作任务的公共类库。
Weiz.TaskManager.Utility					5. 整个平台的公共类库
Weiz.TaskManager.Models
Weiz.TaskManager.BLL
Weiz.TaskManager.DAL

3.  数据库
在\Documents 目录下 找到”SQL合并脚本_20150911.sql”执行创建相关表和初始数据。

4.	系统配置
系统中所有的作业信息，都存储在数据库中。无论是后台管理添加，修改相关的作业配置，到数据库，然后window 宿主服务，启动时，通过配置的job，完成作业的初始化和调度。
 



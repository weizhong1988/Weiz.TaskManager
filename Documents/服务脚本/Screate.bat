@echo off
@echo 安装服务... 
%~dp0\Weiz.TaskManager.ServiceBus.exe  -instance "Weiz.TaskManager.ServiceBus"  -servicename "Weiz.TaskManager.ServiceBus" -description "SWeiz.TaskManager.ServiceBus" -displayname "Weiz.TaskManager.ServiceBus"  install 

@echo 安装完成.
@pause
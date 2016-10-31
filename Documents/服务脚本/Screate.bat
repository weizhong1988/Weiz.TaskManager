@echo off
@echo 安装服务... 
%~dp0\SFO2O.ServiceBus.exe  -instance "SFO2O.ServiceBus"  -servicename "SFO2O.ServiceBus" -description "SFO2O.ServiceBus" -displayname "SFO2O.ServiceBus"  install 

@echo 安装完成.
@pause
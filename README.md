# Link-ToDo

这是一款基于wpf应用开发的模仿Microsoft Todo的todo应用。

可对每条todo添加关联人提醒，定时发送邮件到关联人提醒待办任务。

数据库采用MySQL服务器，图片资源的存储采用七牛云对象存储。

## 界面展示

![image-20221223214939703](http://src.star-tears.cn/img-bed/linktodo-202212232149764.png)

![image-20221223215030535](http://src.star-tears.cn/img-bed/linktodo-202212232150595.png)

## 使用说明

### 数据库部分

MySQL数据库创建两张表，todoinfo表和userinfo表，两表字段设置如下：

#### todoinfo表

| 字段名称 | 数据类型 | 长度 | 字段含义                           | 是否主键 |
| -------- | -------- | ---- | ---------------------------------- | -------- |
| uuid     | varchar  | 64   | 单条todo的UUID，不可更改，唯一编号 | 是       |
| content  | varchar  | 1024 | 单条todo的内容                     | 否       |
| date     | datetime | /    | 提醒日期                           | 否       |
| priority | int      | 11   | 优先级                             | 否       |
| isdone   | int      | 11   | 是否已完成                         | 否       |
| teammate | varchar  | 1024 | 关联人列表                         | 否       |

#### userinfo表

| 字段名称 | 数据类型 | 长度 | 字段含义       | 是否主键 |
| -------- | -------- | ---- | -------------- | -------- |
| uuid     | varchar  | 64   | 通讯录成员UUID | 是       |
| name     | varchar  | 255  | 成员姓名       | 否       |
| phone    | varchar  | 64   | 成员电话号码   | 否       |
| email    | varchar  | 64   | 成员电子邮箱   | 否       |
| imgpath  | varchar  | 255  | 头像路径后缀   | 否       |

### 图床部分

本项目采用七牛云存储服务来存储图片资源，可在QiniuBase.cs里修改相关前缀路径和桶名称

### 全局设置

由于防止暴露个人信息，全局设置脚本并未上传，若想自行编译运行该项目，可在Myscripts文件夹内创建`Settings.cs`脚本，按如下模板填写相关信息：

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class Settings
    {
        // 数据库名
        public static string DatbaseName { get; } = "linktodo";
        // 数据库所在服务器域名或ip
        public static string DatabaseHost { get; } = "";
        // 数据库端口号
        public static string DatabasePort { get; } = "3306";
        // 数据库用户名
        public static string DatabaseUsername { get; } = "linktodo";
        // 数据库密码
        public static string DatabasePassword { get; } = "";

        // 七牛云密钥对
        public static string QiniuAccessKey { get; } = "";
        public static string QiniuSecretKey { get; } = "";


        //邮件服务发送方
        public static string EmailFrom { get; } = "xxxxxxxxx@qq.com";
        //邮件服务发送方授权码
        public static string EmailPwd { get; } = "ajrtpsdsdsncfdgz";
    }
}

```


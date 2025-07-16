

简易的命令行入门教程:
Git 全局设置:

git config --global user.name "美哉"
git config --global user.email "494436488@qq.com"
创建 git 仓库:

mkdir unity
cd unity
git init 
touch README.md
git add README.md
git commit -m "first commit"
git remote add origin https://gitee.com/meizai55/unity.git
git push -u origin "master"
已有仓库?

cd existing_git_repo
git remote add origin https://gitee.com/meizai55/unity.git
git push -u origin "master"



自动提交8888
策士


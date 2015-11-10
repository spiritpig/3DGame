#!/bin/bash

#
#目前commit的message不支持带有空格的字符串.
#所以，需要分隔两端字符串时
#请用其他分隔符如-
#

git add -a ProjectSettings
git add -a Assets/Resources/Font
git add -a Assets/Resources/Font.meta
git add -a Assets/Resources/Mesh
git add -a Assets/Resources/Mesh.meta
git add -a Assets/Resources/Effect
git add -a Assets/Resources/Effect.meta
git add -a Assets/Resources/Material
git add -a Assets/Resources/Material.meta
git add -a Assets/Resources/Prefab
git add -a Assets/Resources/Prefab.meta
git add -a Assets/Resources/Sprite
git add -a Assets/Resources/Sprite.meta
git add -a Assets/Resources/order.txt
git add -a Assets/Resources/order.txt.meta
git add -a Assets/Scene
git add -a Assets/Scene.meta
git add -a Assets/Script
git add -a Assets/Script.meta
git add "Assets/New Terrain 1.asset"
git add "Assets/New Terrain 1.asset.meta"
git commit -am "$@"
git push origin master

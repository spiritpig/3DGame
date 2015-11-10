#!/bin/bash

#
#目前commit的message不支持带有空格的字符串.
#所以，需要分隔两端字符串时
#请用其他分隔符如-
#

git add -A ProjectSettings
git add -A Assets/Resources/Font
git add -A Assets/Resources/Font.meta
git add -A Assets/Resources/Mesh
git add -A Assets/Resources/Mesh.meta
git add -A Assets/Resources/Effect
git add -A Assets/Resources/Effect.meta
git add -A Assets/Resources/Material
git add -A Assets/Resources/Material.meta
git add -A Assets/Resources/Prefab
git add -A Assets/Resources/Prefab.meta
git add -A Assets/Resources/Sprite
git add -A Assets/Resources/Sprite.meta
git add -A Assets/Resources/order.txt
git add -A Assets/Resources/order.txt.meta
git add -A Assets/Scene
git add -A Assets/Scene.meta
git add -A Assets/Script
git add -A Assets/Script.meta
git add "Assets/New Terrain 1.asset"
git add "Assets/New Terrain 1.asset.meta"
git commit -am "$@"
git push origin master

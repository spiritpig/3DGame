#!/bin/bash

#
#目前commit的message不支持带有空格的字符串.
#所以，需要分隔两端字符串时
#请用其他分隔符如-
#

git add ProjectSettings
git add Assets/Resources/Font
git add Assets/Resources/Font.meta
git add Assets/Resources/Mesh
git add Assets/Resources/Mesh.meta
git add Assets/Resources/Effect
git add Assets/Resources/Effect.meta
git add Assets/Resources/Material
git add Assets/Resources/Material.meta
git add Assets/Resources/Prefab
git add Assets/Resources/Prefab.meta
git add Assets/Resources/Sprite
git add Assets/Resources/Sprite.meta
git add Assets/Resources/order.txt
git add Assets/Resources/order.txt.meta
git add Assets/Scene
git add Assets/Scene.meta
git add Assets/Script
git add Assets/Script.meta
git add "Assets/New Terrain 1.asset"
git add "Assets/New Terrain 1.asset.meta"
git commit -am "$@"
git push origin master

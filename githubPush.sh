#!/bin/bash

git add Assets/Resources/Prefab
git add Assets/Resources/Prefab.meta
git add Assets/Resources/Sprite
git add Assets/Resources/Sprite.meta
git add Assets/Scene
git add Assets/Scene.meta
git add Assets/Script
git add Assets/Script.meta
git add "Assets/New Terrain 1.asset"
git add "Assets/New Terrain 1.asset.meta"
git commit -am "$@"
git push origin master

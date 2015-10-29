#!/bin/bash

git add Assets/Resources/Prefab
git add Assets/Resources/Prefab.meta
git add Assets/Resources/Scene
git add Assets/Resources/Scene.meta
git add Assets/Resources/Script
git add Assets/Resources/Script.meta
git add Assets/Resources/Sprite
git add Assets/Resources/Sprite.meta
git commit -am "$@"
git push origin master

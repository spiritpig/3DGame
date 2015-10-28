#!/bin/bash

git add Assets/Prefab
git add Assets/Prefab.meta
git add Assets/Scene
git add Assets/Scene.meta
git add Assets/Script
git add Assets/Script.meta
git add Assets/Sprite
git add Assets/Sprite.meta
git commit -m "$@"
git push origin master

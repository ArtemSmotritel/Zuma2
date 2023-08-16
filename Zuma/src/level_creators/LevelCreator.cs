using System;
using Zuma.models;
using Zuma.src.frog;
using Zuma.src.level;

namespace Zuma.src.level_creators
{
    public abstract class LevelCreator
    {
        public Level Create() => new Level(GetName(), GetNumber(), GetBackgroundURI(), GetFrog(), GetPath(), GetEnemyCount());

        protected abstract string GetName();
        protected abstract int GetNumber();
        protected abstract Uri GetBackgroundURI();
        protected abstract Frog GetFrog();
        protected abstract Path GetPath();
        protected abstract int GetEnemyCount();
    }
}

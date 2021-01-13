using System;

namespace Main
{
    [Serializable]
    public class BonnouEntity
    {
        public int Id;
        public string Theme;
        public string Ruby;
        public string Description;
        public int Rank;
        public string Genre;
        public float SpeedRate = 1;
        public int Score;
    }
}
namespace System.Collections.Generic
{
    using UnityEngine;

    public static class ListExtension
    {
        public static List<T> Shuffle<T>(this List<T> listToShuffle)
        {
            List<T> shuffledList = new List<T>();

            int n = listToShuffle.Count;
            while (n > 1)
            {
                int i = Random.Range(0, listToShuffle.Count);

                shuffledList.Add(listToShuffle[i]);
                listToShuffle.Remove(listToShuffle[i]);

                n++;
            }

            return shuffledList;
        }
    }
}


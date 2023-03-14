using System;
using System.Collections.Generic;

public class Chunker {
    public static IEnumerable<string> ChunkIter(string s, int chunks) {
        if (chunks == 0) {
            throw new ArgumentException("chunks can't be zero");
        }

        if (s.Length == 0 && chunks > 0) {
            yield return "";
        }

        if (chunks == 1) {
            yield return s;
        }

        if (chunks > s.Length) {
            chunks = s.Length;
        }

        if (chunks == s.Length) {
            var stringArr = s.Split();
            foreach (var str in stringArr) {
                yield return str;
            }

            // s.Split().ForEach(x => yield return x);
            // yield return s.Split();
        }

        int start = 0;
        var rounding = new[] { MidpointRounding.ToPositiveInfinity,
                               MidpointRounding.ToNegativeInfinity };
        int r = 0;
        while (start < s.Length) {
            int chunkSize = (int)Math.Round((double)(s.Length - start) / chunks,
                                            rounding[r]);
            r = 1 - r;  // Swap the rounding
            yield return s.Substring(start, chunkSize);
            start += chunkSize;
            chunks--;
        }
    }

    public static string[] Chunk(string s, int chunks) {
        if (chunks == 0) {
            throw new ArgumentException("chunks can't be zero");
        }

        if (s.Length == 0 && chunks > 0) {
            return new string[0];
        }
        var res = new string[chunks];
        if (chunks == 1) {
            res[0] = s;
            return res;
        }

        if (chunks > s.Length) {
            chunks = s.Length;
        }

        if (chunks == s.Length) {
            return s.Split();
        }

        var ret = new List<string>(chunks);
        int chunkSize = s.Length / chunks;
        int remainder = s.Length % chunks;
        int start = 0;
        while (start < s.Length) {
            int thisChunkSize = chunkSize;
            if (remainder > 0) {
                thisChunkSize++;
                remainder--;
            }
            ret.Add(s.Substring(start, thisChunkSize));
            start += thisChunkSize;
        }

        return ret.ToArray();
    }
}

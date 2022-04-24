using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hamsa
{
    class Hand
    {
        public Dictionary<int, Dictionary<string, float>> handKeyPoints;
        public bool detected { get; set; }
        public Hand(byte[] keypoints)
        {
            handKeyPoints = new Dictionary<int, Dictionary<string, float>>();
            int index = 0;

            // if the values are -1, that means no hand was detected
            detected = BitConverter.ToSingle(keypoints, index) != -1;            
            
            // init handsKeyPoints
            for (int i= 0; index < keypoints.Length; i++)
            {
                handKeyPoints[i] = new Dictionary<string, float>();
                handKeyPoints[i]["X"] = 1 - BitConverter.ToSingle(keypoints, index);
                handKeyPoints[i]["Y"] = BitConverter.ToSingle(keypoints, index + 4);
                index += 8;
            }
        }
    }
}

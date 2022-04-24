using System;
using System.Collections.Generic;
using System.Drawing;

namespace Hamsa
{
    /// <summary>
    /// Hand class has the results from MediaPipe accepting with an buffer. This calss has
    /// different useful functions for easier work with the data.
    /// </summary>
    class Hand
    {
        /// <summary>
        /// Dictionary containing more Dictionarys which represent the different points of the hand.
        /// </summary>
        public Dictionary<int, Dictionary<string, float>> handKeyPoints;

        
        /// <summary>
        /// Whether a hand were detected in the data given to the constructor.
        /// </summary>
        public bool detected { get; set; }


        /// <summary>
        /// keypoints should be a buffer with size multiple of 8, each 8 bytes are the X value 
        /// and Y value
        /// </summary>
        /// <param name="keypoints">buffer contatining the data</param>
        public Hand(byte[] keypoints)
        {
            handKeyPoints = new Dictionary<int, Dictionary<string, float>>();
            int index = 0;

            // Data is messed up
            if(keypoints.Length % 8 != 0)
            {
                detected = false;
                return;
            }

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

        /// <param name="finger">Value of the Fingers enum.</param>
        /// <returns>Dictionary conatining the data about the tip of the finger</returns>
        public Dictionary<string, float> GetFinger(Fingers finger)
        {
            switch(finger)
            {
                case Fingers.Thumb:
                    return GetThumbFinger();
                case Fingers.Index:
                    return GetIndexFinger();
                case Fingers.Middle:
                    return GetMiddleFinger();
                case Fingers.Ring:
                    return GetRingFinger();
                case Fingers.Pinky:
                    return GetPinkyFinger();
                default:
                    throw new Exception("CantReturnFinger");
            }
        }

        public Dictionary<string, float> GetThumbFinger()
        {
            return new Dictionary<string, float>(handKeyPoints[4]);
        }

        public Dictionary<string, float> GetIndexFinger()
        {
            return new Dictionary<string, float>(handKeyPoints[8]);
        }

        public Dictionary<string, float> GetMiddleFinger()
        {
            return new Dictionary<string, float>(handKeyPoints[12]);
        }

        public Dictionary<string, float> GetRingFinger()
        {
            return new Dictionary<string, float>(handKeyPoints[16]);
        }
        public Dictionary<string, float> GetPinkyFinger()
        {
            return new Dictionary<string, float>(handKeyPoints[20]);
        }
    }
}

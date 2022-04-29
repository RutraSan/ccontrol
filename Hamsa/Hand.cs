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
        public Dictionary<int, Dictionary<string, double>> handKeyPoints;

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
            handKeyPoints = new Dictionary<int, Dictionary<string, double>>();
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
                handKeyPoints[i] = new Dictionary<string, double>();
                handKeyPoints[i]["X"] = 1 - BitConverter.ToSingle(keypoints, index);
                handKeyPoints[i]["Y"] = BitConverter.ToSingle(keypoints, index + 4);
                index += 8;
            }
        }

        /// <summary>
        /// Returns whether the finger is up or not.
        /// </summary>
        /// <param name="finger"></param>
        public bool IsFingerUp(Fingers finger)
        {
            var fingerJoints = GetFinger(finger);
            var dis = fingerJoints["tip"]["Y"] - fingerJoints["pip"]["Y"];
            return fingerJoints["tip"]["Y"] < fingerJoints["pip"]["Y"];
        }

        /// <summary>
        /// Returns the specified finger.
        /// </summary>
        /// <param name="finger">Value of the Fingers enum.</param>
        /// <returns>Dictionary conatining the data about the tip of the finger</returns>
        public Dictionary<string, Dictionary<string, double>> GetFinger(Fingers finger)
        {
            int index = 1 + (int)finger * 4;
            var fingerJoints = new Dictionary<string, Dictionary<string, double>>();
            fingerJoints["mcp"] = handKeyPoints[index];
            fingerJoints["pip"] = handKeyPoints[index + 1];
            fingerJoints["dip"] = handKeyPoints[index + 2];
            fingerJoints["tip"] = handKeyPoints[index + 3];
            return fingerJoints;
        }
    }
}

import cv2
import mediapipe as mp
import win32pipe, win32file
import struct

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_hands = mp.solutions.hands

TMP_IMG = 'C:\\temp\\hamsaimg.jpeg'

# pipe for communicating with the server
pipe = win32pipe.CreateNamedPipe(
    r'\\.\pipe\HandDetection',
    win32pipe.PIPE_ACCESS_OUTBOUND,
    win32pipe.PIPE_TYPE_BYTE | win32pipe.PIPE_READMODE_BYTE | win32pipe.PIPE_WAIT,
    1, 65536, 65536,
    10,
    None)


# webcam
cap = cv2.VideoCapture(0)

with mp_hands.Hands(
    model_complexity=0,
    min_detection_confidence=0.5,
    min_tracking_confidence=0.5) as hands:
  while cap.isOpened():
    success, image = cap.read()
    if not success:
      # If loading a video, use 'break' instead of 'continue'.
      continue

    # To improve performance, optionally mark the image as not writeable to
    # pass by reference.
    image.flags.writeable = False
    image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)
    results = hands.process(image)

    # Draw the hand annotations on the image.
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
    if results.multi_hand_landmarks:
      for hand_landmarks in results.multi_hand_landmarks:
        mp_drawing.draw_landmarks(
            image,
            hand_landmarks,
            mp_hands.HAND_CONNECTIONS,
            mp_drawing_styles.get_default_hand_landmarks_style(),
            mp_drawing_styles.get_default_hand_connections_style())

    # Flip the image horizontally for a selfie-view display.
    image = cv2.flip(image, 1)
    cv2.imwrite(TMP_IMG, image)

    # save data as binary dump
    keypoints = []
    buffer = struct.pack("f", -1)
    if results.multi_hand_landmarks is not None:
        buffer = b''
        for data_point in results.multi_hand_landmarks[0].landmark:
            buffer += struct.pack("f f", data_point.x, data_point.y)

    # send the data
    win32file.WriteFile(pipe, buffer)
cap.release()
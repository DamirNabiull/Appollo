import cv2
import time

time_sum = 0
frames = 0

fourcc = cv2.VideoWriter_fourcc('M','J','P','G')
cap = cv2.VideoCapture(0)
cap.open(1 + cv2.CAP_DSHOW)
cap.set(cv2.CAP_PROP_FOURCC, fourcc)
cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
cap.set(cv2.CAP_PROP_FPS, 60)

while(1):
    before_timer = time.perf_counter()
    ret, frame = cap.read()
    if ret is None:
        print("Frame is empty")
        break
    else:
        image = cv2.rotate(frame, cv2.ROTATE_90_CLOCKWISE)
        cv2.imshow('VIDEO', frame)
        after_timer = time.perf_counter() - before_timer
        time_sum += after_timer
        frames += 1
        if frames % 30 == 0:
            print("{} per second".format(frames/time_sum))
        cv2.waitKey(1)
# import the opencv library
import cv2
import requests
import base64
from multiprocessing import Process

URL = "http://localhost:3001/play/"

# define a video capture object
vid = cv2.VideoCapture(0)

while (True):
    ret, frame = vid.read()
    image = cv2.rotate(frame, cv2.ROTATE_90_CLOCKWISE)
    retval, buffer = cv2.imencode('.jpg', frame)
    jpg_as_text = base64.b64encode(buffer)
    cv2.imshow('frame', frame)
    response = requests.post(URL, data=jpg_as_text)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break


vid.release()
cv2.destroyAllWindows()
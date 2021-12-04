import cv2
import time
import base64
import requests
import socket
import binascii
from multiprocessing import Process, Manager, Value
from ctypes import c_char_p

host = "127.0.0.1"
port = 11000
time_sum = 0
frames = 0
connected = True
jpg_as_text = ''
URL = "http://localhost:3001/play/"

# cap.open(1 + cv2.CAP_DSHOW)
# cap.set(cv2.CAP_PROP_FOURCC, fourcc)
# cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
# cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
# cap.set(cv2.CAP_PROP_FPS, 60)


def send_via_socket(string, url, host, port):
    print(host, port)
    while 1:
        try:
            sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            sock.connect((host, port))
            print(url, string.value)
            print(bytearray(binascii.a2b_base64(string.value)))
            # response = requests.post(url, data=string.value)
            sock.send(bytearray(binascii.a2b_base64(string.value))+bytearray('<EOF>'.encode()))
            sock.close()
        except Exception as e:
            # print(bytearray(binascii.a2b_base64(string.value)))
            print(e)



def get_image(string):
    fourcc = cv2.VideoWriter_fourcc('M', 'J', 'P', 'G')
    cap = cv2.VideoCapture(0)
    time.sleep(1.000)
    while 1:
        ret, frame = cap.read()
        if ret is None:
            print("Frame is empty")
            break
        else:
            image = cv2.rotate(frame, cv2.ROTATE_90_CLOCKWISE)
            retval, buffer = cv2.imencode('.jpg', image)
            base64_message = base64.b64encode(buffer)
            base64_bytes = base64_message.encode('ascii')
            message_bytes = base64.b64decode(base64_bytes)
            message = message_bytes.decode('ascii')
            cv2.imshow('frame', image)

            if cv2.waitKey(1) & 0xFF == ord('q'):
                break


if __name__ == '__main__':
    manager = Manager()
    string = manager.Value(c_char_p, '')
    p1 = Process(target=send_via_socket, args=(string, URL, host, port,))
    p1.start()
    p2 = Process(target=get_image, args=(string,))
    p2.start()
    p1.join()
    p2.join()

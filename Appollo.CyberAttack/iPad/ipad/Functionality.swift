//
//  Functionality.swift
//  ipad
//
//  Created by Anna Dluzhinskaya on 23.09.2021.
//

import Foundation
import SwiftUI

func HttpRequest(urlStr: String, videoName: String, videoTime: Int) -> Void {
    let tempUrlStr = "http://" + urlStr + ":3001/play/?video=" + videoName + ".mp4"
    let url = URL(string: tempUrlStr)!

    let task = URLSession.shared.dataTask(with: url) {(data, response, error) in
        guard let data = data else { return }
        print(String(data: data, encoding: .utf8)!)
    }
    task.resume()
//    Thread.sleep(forTimeInterval: videoTime)
}

func HttpStopRequest(urlStr: String) -> Void {
    let tempUrlStr = "http://" + urlStr + ":3001/play/?video=stop"
    let url = URL(string: tempUrlStr)!

    let task = URLSession.shared.dataTask(with: url) {(data, response, error) in
        guard let data = data else { return }
        print(String(data: data, encoding: .utf8)!)
    }
    task.resume()
}

func HttpResumeRequest(urlStr: String) -> Void {
    let tempUrlStr = "http://" + urlStr + ":3001/play/?video=resume"
    let url = URL(string: tempUrlStr)!

    let task = URLSession.shared.dataTask(with: url) {(data, response, error) in
        guard let data = data else { return }
        print(String(data: data, encoding: .utf8)!)
    }
    task.resume()
}

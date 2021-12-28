//
//  Functionality.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 15.12.2021.
//

import Foundation
import SwiftUI

func HttpRequest(urlStr: String, videoName: String) -> Void {
    let tempUrlStr = "http://" + urlStr + ":3001/play/?video=" + videoName
    let url = URL(string: tempUrlStr)!

    let task = URLSession.shared.dataTask(with: url) {(data, response, error) in
        guard let data = data else { return }
        print(String(data: data, encoding: .utf8)!)
    }
    task.resume()
}

func HttpStopRequest(urlStr: String) -> Void {
    let tempUrlStr = "http://" + urlStr + ":3001/play/?video=idle"
    let url = URL(string: tempUrlStr)!

    let task = URLSession.shared.dataTask(with: url) {(data, response, error) in
        guard let data = data else { return }
        print(String(data: data, encoding: .utf8)!)
    }
    task.resume()
}

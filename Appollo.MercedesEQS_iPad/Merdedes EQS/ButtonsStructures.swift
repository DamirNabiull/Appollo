//
//  ButtonsStructures.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 15.12.2021.
//

import Foundation
import SwiftUI

struct settingsButton: View {
    @Binding var inSettings: Bool
    
    var body: some View {
        Button(action: {
            
        }) {
            Text("")
                .frame(width: 200, height: 150)
        }
        .simultaneousGesture(LongPressGesture(minimumDuration: 3.0).onEnded { _ in
            self.inSettings.toggle()
        })
    }
    
}

struct HomeButton: View {
    @Binding var urlStr: String
    @Binding var _go: Bool
    @Binding var _formOn: Bool
    @Binding var window: Int
    @Binding var inCancel: Bool
    
    var body: some View {
        Button {
            if self.window == 0 {
                DispatchQueue.global().async {
                    self.inCancel = true
                }
            } else {
                DispatchQueue.global().async {
                    self.window = 0
                    HttpStopRequest(urlStr: urlStr)
                }
            }
        }label: {
            Image("Button_Home")
                .resizable()
                .frame(width: 60, height: 60.15)
                .ignoresSafeArea(.all)
        }
        .position(x: 63, y: 65)
    }
}

struct PlayButton:  View{
    var urlStr: String
    var videoName: String
    var index: Int
    @Binding var window: Int
    
    var body: some View {
        Button{
            DispatchQueue.global().async {
                self.window = index
                HttpRequest(urlStr: urlStr, videoName: videoName)
            }
        }label: {
            Image("Button_Play")
                .resizable()
                .frame(width: 80, height: 80)
                .ignoresSafeArea(.all)
        }
    }
}

struct ContinueButton: View {
    @Binding var _name: String
    @Binding var _mail: String
    @Binding var _phone: String
    @Binding var _go: Bool
    
    @State private var csvText = ""
    
    var body: some View {
        let file: String = "DataEQS.csv"
        let fileManager = FileManager.default
        Button {
            UIApplication.shared.endEditing()
            if _name.count > 5 && _mail.count > 5 && _phone.count > 5 {
                DispatchQueue.global().async {
                    csvText = self._name+","+self._mail+","+self._phone+"\n"
                    do {
                        let path = try fileManager.url(for: .documentDirectory, in: .userDomainMask, appropriateFor: nil, create: true)
                        let fileURL = path.appendingPathComponent(file)
                        try csvText.appendLineToURL(fileURL: fileURL as URL)
                    } catch {
                        print("error creating file")
                    }
                    Thread.sleep(forTimeInterval: 0.1)
                    self._name = ""
                    self._phone = ""
                    self._mail = ""
                    _go.toggle()
                }
            }
        }label: {
            Image("Button_Continue")
                .resizable()
                .frame(width: 162.5, height: 32)
                .ignoresSafeArea(.all)
        }
        .offset(y: 570)
    }
}

extension String {
    func appendLineToURL(fileURL: URL) throws {
         try (self + "\n").appendToURL(fileURL: fileURL)
     }

     func appendToURL(fileURL: URL) throws {
         let data = self.data(using: String.Encoding.utf8)!
         try data.append(fileURL: fileURL)
     }
 }

 extension Data {
     func append(fileURL: URL) throws {
         if let fileHandle = FileHandle(forWritingAtPath: fileURL.path) {
             defer {
                 fileHandle.closeFile()
             }
             fileHandle.seekToEndOfFile()
             fileHandle.write(self)
         }
         else {
             try write(to: fileURL, options: .atomic)
         }
     }
 }

extension UIApplication {
    func endEditing() {
        sendAction(#selector(UIResponder.resignFirstResponder), to: nil, from: nil, for: nil)
    }
}

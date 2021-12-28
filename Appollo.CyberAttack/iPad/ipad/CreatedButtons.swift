//
//  CreatedButtons.swift
//  ipad
//
//  Created by Anna Dluzhinskaya on 23.09.2021.
//

import Foundation
import SwiftUI

struct buttonWithBorder:  View{
    var urlStr: String
    var videoName: String
    var videoTime: Int
    var text: String
    @Binding var inProgress: Bool
    @Binding var timeRemaining: Int
    @State private var tapped = false
    
    var body: some View {
        Button{
            DispatchQueue.global().async {
                self.tapped.toggle()
                Thread.sleep(forTimeInterval: 0.5)
                self.inProgress.toggle()
                self.timeRemaining = videoTime
                HttpRequest(urlStr: urlStr, videoName: videoName, videoTime: videoTime)
                self.tapped.toggle()
//                self.inProgress.toggle()
            }
        }label: {
            Text(text)
                .frame(width: 962, height: 116)
                .background(tapped ? Color.red : Color("buttonBackground"))
                .foregroundColor(Color.white)
                .font(Font.custom("TTHoves-Bold", size: 40))
                .overlay(
                    RoundedRectangle(cornerRadius: 22)
                        .stroke(Color.white, lineWidth: 2))
                .cornerRadius(22)
        }
    }
}

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

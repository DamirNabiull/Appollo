//
//  MyScreens.swift
//  ipad
//
//  Created by Anna Dluzhinskaya on 23.09.2021.
//

import Foundation
import SwiftUI

struct InProgressView: View{
    var urlStr: String
    @Binding var inPause: Bool
    @Binding var timeRemaining: Int
    @State private var tapped = false
    
    var body: some View {
        ZStack {
            Rectangle()
                .frame(width: 1024, height: 670, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                .foregroundColor(Color("appBackground"))
            VStack {
                Text("Watch the attack on the video wall ⬆️")
                    .multilineTextAlignment(.center)
                    .frame(width: 700, height: 670, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                    .foregroundColor(.white)
                    .font(Font.custom("TTHoves-Bold", size: 70))
                    .background(Color("appBackground"))
                    .padding(.top, -252)
                Button{
                    DispatchQueue.global().async {
                        if !self.inPause {
                            self.inPause.toggle()
                            self.tapped.toggle()
                            Thread.sleep(forTimeInterval: 0.5)
                            HttpStopRequest(urlStr: urlStr)
                        } else {
                            self.inPause.toggle()
                            self.tapped.toggle()
                            Thread.sleep(forTimeInterval: 0.5)
                            HttpResumeRequest(urlStr: urlStr)
                        }
                    }
                }label: {
                    Text("Pause")
                        .frame(width: 450, height: 116)
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
    }
}

struct InSettingsView: View{
    @Binding var urlStr: String
    @Binding var inSettings: Bool
    
    var body: some View {
        ZStack {
            RoundedRectangle(cornerRadius: 70)
                .frame(width: 900, height: 700, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                .foregroundColor(.blue)
            VStack(spacing: 80) {
                TextField("Enter IP", text: $urlStr)
                    .multilineTextAlignment(.center)
                    .frame(width: 500, height: 200, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                    .foregroundColor(Color.white)
                    .font(Font.custom("TTHoves-Bold", size: 70))
                    .overlay(RoundedRectangle(cornerRadius: 50)
                        .stroke(Color.white, lineWidth: 5))
                Button {
                    UserDefaults.standard.setValue(self.urlStr, forKey: "urlStr")
                    self.inSettings.toggle()
                }label: {
                    Text("Save")
                        .frame(width: 350, height: 150)
                        .background(Color.green)
                        .foregroundColor(Color.white)
                        .font(Font.custom("TTHoves-Bold", size: 70))
                        .overlay(
                            RoundedRectangle(cornerRadius: 22)
                                .stroke(Color.white, lineWidth: 2))
                        .cornerRadius(22)
                }
            }
        }
    }
}

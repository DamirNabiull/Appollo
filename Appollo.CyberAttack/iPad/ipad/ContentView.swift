//
//  ContentView.swift
//  ipad
//
//  Created by Anna Dluzhinskaya on 19.09.2021.
//

import SwiftUI

struct ContentView: View {
    @State private var timeRemaining = 10
    private let timer = Timer.publish(every: 1, on: .main, in: .common).autoconnect()
    @State private var inSettings = false
    @State private var inProgress = false
    @State private var inPause = false
    @State private var urlStr: String = UserDefaults.standard.string(forKey: "urlStr") ?? "192.168.0.0"

    var body: some View {

        ZStack {
            Image("BackgroundImage")
                .resizable()
                .edgesIgnoringSafeArea(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .onReceive(timer, perform: { _ in
                    if inProgress && !inPause {
                        if timeRemaining > 0 {
                            timeRemaining -= 1
                        } else {
                            inProgress = false
                        }
                    }
                })


            VStack (alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/, spacing: 39){
                Spacer()
                    .frame(height:401)

                buttonWithBorder(urlStr: urlStr, videoName: "1", videoTime: 90, text: "Oil company", inProgress: $inProgress, timeRemaining: $timeRemaining)

                buttonWithBorder(urlStr: urlStr, videoName: "2", videoTime: 84, text: "Power plant", inProgress: $inProgress, timeRemaining: $timeRemaining)

                buttonWithBorder(urlStr: urlStr, videoName: "3", videoTime: 90, text: "IT company", inProgress: $inProgress, timeRemaining: $timeRemaining)

                Spacer()

                settingsButton(inSettings: $inSettings)
            }

            inProgress ? InProgressView(urlStr: urlStr, inPause: $inPause, timeRemaining: $timeRemaining) : nil

            inSettings ? InSettingsView(urlStr: $urlStr, inSettings: $inSettings) : nil
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}


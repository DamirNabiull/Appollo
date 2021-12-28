//
//  ContentView.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 14.12.2021.
//

import SwiftUI

struct ContentView: View {
    @State private var _name: String = ""
    @State private var _mail: String = ""
    @State private var _phone: String = ""
    @State private var _go: Bool = false
    @State private var _formOn = false
    @State private var window: Int = 0
    @State private var inSettings = false
    @State private var inCancel = false
    @State private var urlStr: String = UserDefaults.standard.string(forKey: "urlStr") ?? "192.168.0.0"
    
    
    
    var body: some View {
        ZStack {
            Image("Background_Start")
                .resizable()
                .ignoresSafeArea(.all)
                .onTapGesture {
                    UIApplication.shared.endEditing()
                }
            
            Image("Background_Norm")
                .resizable()
            
            if _go || _formOn == false {
                if window == 0 {
                    MenuView(urlStr: $urlStr, window: $window)
                } else if window == 1 {
                    Window1()
                } else if window == 2 {
                    Window2()
                } else if window == 3 {
                    Window3()
                }
                if _formOn || (_formOn == false && window > 0){
                    HomeButton(urlStr: $urlStr, _go: $_go, _formOn: $_formOn, window: $window, inCancel: $inCancel)
                }
                
            } else {
                StartView(_name: $_name, _mail: $_mail, _phone: $_phone, _go: $_go)
            }
            
            if window == 0 {
                VStack(alignment: .center, spacing: 0) {
                    settingsButton(inSettings: $inSettings)
                    Spacer()
                }
                
                inSettings ? InSettingsView(urlStr: $urlStr, inSettings: $inSettings, _formOn: $_formOn) : nil
            }
            
            inCancel ? Cancel(_go: $_go, inCancel: $inCancel) : nil
 
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        Group {
            ContentView()
        }
    }
}

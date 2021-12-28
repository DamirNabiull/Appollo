//
//  Screens.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 15.12.2021.
//

import Foundation
import SwiftUI

struct Cancel: View {
    @Binding var _go: Bool
    @Binding var inCancel: Bool
    
    var body: some View {
        ZStack {
            Rectangle()
                .frame(minWidth: 0, maxWidth: .infinity, minHeight: 0, maxHeight: .infinity)
                .foregroundColor(Color("Color_Shadow"))
                .ignoresSafeArea(.all)
            ZStack {
                Rectangle()
                    .frame(width: 553, height: 294)
                    .foregroundColor(.white)
                VStack(spacing: 30) {
                    Text("Вы действительно хотите завершить сеанс?")
                        .multilineTextAlignment(.center)
                        .frame(width: 344, height: 100)
                        .foregroundColor(Color.black)
                        .font(Font.custom("RobotoCondensed-Regular", size: 28))
                    HStack(spacing: 30) {
                        Button {
                            DispatchQueue.global().async {
                                self.inCancel = false
                                self._go.toggle()
                            }
                        }label: {
                            Image("Button_Accept")
                                .resizable()
                                .frame(width: 162.5, height: 32)
                                .ignoresSafeArea(.all)
                        }
                        
                        Button {
                            DispatchQueue.global().async {
                                self.inCancel = false
                            }
                        }label: {
                            Image("Button_Decline")
                                .resizable()
                                .frame(width: 162.5, height: 32)
                                .ignoresSafeArea(.all)
                        }
                    }
                }
            }
            .offset(y: -115)
        }

    }
}

struct InSettingsView: View{
    @Binding var urlStr: String
    @Binding var inSettings: Bool
    @Binding var _formOn: Bool
    
    var body: some View {
        ZStack {
            RoundedRectangle(cornerRadius: 70)
                .frame(width: 500, height: 500, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                .foregroundColor(.blue)
            VStack(spacing: 30) {
                TextField("Enter IP", text: $urlStr)
                    .multilineTextAlignment(.center)
                    .frame(width: 400, height: 100, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                    .foregroundColor(Color.white)
                    .font(Font.custom("RobotoCondensed-Regular", size: 30))
                    .overlay(RoundedRectangle(cornerRadius: 50)
                        .stroke(Color.white, lineWidth: 5))
                Toggle("Форма с данными", isOn: $_formOn)
                    .frame(width: 300, height: /*@START_MENU_TOKEN@*/100/*@END_MENU_TOKEN@*/, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                    .foregroundColor(.white)
                Button {
                    UserDefaults.standard.setValue(self.urlStr, forKey: "urlStr")
                    self.inSettings.toggle()
                }label: {
                    Text("Save")
                        .frame(width: 100, height: 50)
                        .background(Color.green)
                        .foregroundColor(Color.white)
                        .font(Font.custom("RobotoCondensed-Regular", size: 30))
                        .overlay(
                            RoundedRectangle(cornerRadius: 22)
                                .stroke(Color.white, lineWidth: 2))
                        .cornerRadius(22)
                }
            }
        }
    }
}


struct StartView: View {
    @Binding var _name: String
    @Binding var _mail: String
    @Binding var _phone: String
    @Binding var _go: Bool
    
    var body: some View {
        VStack(alignment: .center, spacing: /*@START_MENU_TOKEN@*/nil/*@END_MENU_TOKEN@*/) {
            CustomTextField(placeholder: "ФИО", text: $_name, width: 472, height: 15.5, x_off: 0, y_off: 414, key_type: .default)
            CustomTextField(placeholder: "E-MAIL", text: $_mail, width: 472, height: 15.5, x_off: 0, y_off: 464, key_type: .emailAddress)
            CustomTextField(placeholder: "ТЕЛЕФОН", text: $_phone, width: 472, height: 15.5, x_off: 0, y_off: 514, key_type: .phonePad)
            Approval()
            ContinueButton(_name: $_name, _mail: $_mail, _phone: $_phone, _go: $_go)
            Spacer()
        }
    }
}


struct MenuView: View {
    @Binding var urlStr: String
    @Binding var window: Int
    
    var body: some View {
        ZStack {
            VStack(alignment: .center, spacing: 0) {
                Spacer()
                ZStack {
                    Image("ButtonBack_1")
                        .resizable()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                    Text("""
ПЕРВЫЙ
ЭЛЕКТРИЧЕСКИЙ
СЕДАН КЛАССА ЛЮКС
ОТ MERCEDES-EQ
""")
                        .multilineTextAlignment(.leading)
                        .frame(width: 678, height: 162, alignment: .topLeading)
                        .foregroundColor(.white)
                        .font(Font.custom("Daimler CAC Regular", size: 30))
                    /*
                    PlayerView1()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                     */
                    PlayButton(urlStr: urlStr, videoName: "1", index: 1, window: $window)
                }
                ZStack {
                    Image("ButtonBack_2")
                        .resizable()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                    Text("HYPERSCREEN")
                        .multilineTextAlignment(.leading)
                        .frame(width: 678, height: 40, alignment: .topLeading)
                        .foregroundColor(.white)
                        .font(Font.custom("Daimler CAC Regular", size: 30))
                    /*
                    PlayerView2()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                    */
                    PlayButton(urlStr: urlStr, videoName: "2", index: 2, window: $window)
                }
                ZStack {
                    Image("ButtonBack_3")
                        .resizable()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                    Text("""
ВПЕЧАТЛЯЮЩИЙ
ЗАПАС ХОДА
""")
                        .multilineTextAlignment(.leading)
                        .frame(width: 678, height: 81, alignment: .topLeading)
                        .foregroundColor(.white)
                        .font(Font.custom("Daimler CAC Regular", size: 30))
                    /*
                    PlayerView3()
                        .frame(minWidth: 0, maxWidth: .infinity, minHeight: 217, maxHeight: 217, alignment: .center)
                        .ignoresSafeArea(.all)
                     */
                    PlayButton(urlStr: urlStr, videoName: "3", index: 3, window: $window)
                }
            }
        }
    }
}

struct Galery: View {
    @State private var ind: Int = 0
    var images_list: [String]
    var images_count: Int
    
    var body: some View {
        VStack {
            ZStack {
                Image(images_list[ind])
                    .resizable()
                    .aspectRatio(contentMode: .fill)
                    .frame(width: 768, height: 494, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
                
                if images_count > 1{
                    Button {
                        self.ind = (self.ind - 1 + images_count) % images_count
                    }label: {
                        Image("Button_Prev")
                            .resizable()
                            .frame(width: 48.10, height: 89.32)
                    }
                    .offset(x: -325, y: -20)
                    
                    Button {
                        self.ind = (self.ind + 1) % images_count
                    }label: {
                        Image("Button_Next")
                            .resizable()
                            .frame(width: 48.10, height: 89.32)
                    }
                    .offset(x: 325, y: -20)
                }
            }
            .offset(x: 0, y: 106)
            Spacer()
        }
    }
}

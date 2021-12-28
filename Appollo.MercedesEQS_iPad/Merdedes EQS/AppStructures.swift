//
//  AppStructures.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 14.12.2021.
//

import Foundation
import SwiftUI

struct CustomTextField: View {
    var placeholder: String
    @Binding var text: String
    
    var width: CGFloat
    var height: CGFloat
    var x_off: CGFloat
    var y_off: CGFloat
    var key_type: UIKeyboardType

    var body: some View {
        TextField("", text: $text)
            .placeholder(when: text.isEmpty) {
                    Text(placeholder)
                        .foregroundColor(.white)
            }
            .multilineTextAlignment(.center)
            .frame(width: width, height: height, alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/)
            .foregroundColor(Color.white)
            .font(Font.custom("RobotoCondensed-Regular", size: 20))
            .offset(x: x_off, y: y_off)
            .fixedSize(horizontal: false, vertical: /*@START_MENU_TOKEN@*/true/*@END_MENU_TOKEN@*/)
            .keyboardType(key_type)
        Rectangle()
            .frame(width: 472, height: 1, alignment: .topLeading)
            .foregroundColor(Color("Color_Line"))
            .accentColor(Color("Color_Line"))
            .offset(x: x_off, y: y_off+10)
    }
}

struct Approval: View {
    var body: some View {
        HStack(alignment: /*@START_MENU_TOKEN@*/.center/*@END_MENU_TOKEN@*/, spacing: /*@START_MENU_TOKEN@*/nil/*@END_MENU_TOKEN@*/) {
            Image("OK")
                .resizable()
                .frame(width: 24.725, height: 20)
                .ignoresSafeArea(.all)
            Text("Даю согласие на обработку персональных данных")
                .frame(width: 320, height: 20)
                .foregroundColor(.white)
                .font(Font.custom("RobotoCondensed-Regular", size: 15))
        }
        .offset(y: 554)
    }
}

extension View {
    func placeholder<Content: View>(
        when shouldShow: Bool,
        alignment: Alignment = .center,
        @ViewBuilder placeholder: () -> Content) -> some View {

        ZStack(alignment: alignment) {
            placeholder().opacity(shouldShow ? 1 : 0)
            self
        }
    }
}

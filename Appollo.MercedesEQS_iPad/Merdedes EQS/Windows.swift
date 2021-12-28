//
//  Windows.swift
//  Merdedes EQS
//
//  Created by Anna Dluzhinskaya on 18.12.2021.
//

import Foundation
import SwiftUI

struct Window2: View {
    private var images: [String] = ["1_2"]
    private var count: Int = 1
    
    var body: some View {
        ZStack {
            Rectangle()
                .ignoresSafeArea(.all)
                .frame(minWidth: 0, maxWidth: .infinity, minHeight: 0, maxHeight: .infinity, alignment: .center)
                .foregroundColor(Color("Color_Back"))
            Galery(images_list: images, images_count: count)
            Image("Logo")
                .resizable()
                .frame(width: 46.5, height: 46.5)
                .position(x: 710.25, y: 56.25)
            
            Text("""
Дисплей MBUX Hyperscreen
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 100, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("Daimler CAC Regular", size: 32.5))
                .offset(x: 10, y: 167)
            
            Text("""
MBUX. Интуиция, которая обостряется с каждой поездкой.

Комфортные будни за рулем EQS гарантирует обучаемый цифровой интерьер.
Интуитивно понятный благодаря проекционному дисплею MBUX с технологией
дополненной реальности и ассистенту салона MBUX.
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 250, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("RobotoCondensed-Regular", size: 17.5))
                .offset(x: 10, y: 292)
        }
    }
}

struct Window3: View {
    private var images: [String] = ["3_1"]
    private var count: Int = 1
    
    var body: some View {
        ZStack {
            Rectangle()
                .ignoresSafeArea(.all)
                .frame(minWidth: 0, maxWidth: .infinity, minHeight: 0, maxHeight: .infinity, alignment: .center)
                .foregroundColor(Color("Color_Back"))
            Galery(images_list: images, images_count: count)
            Image("Logo")
                .resizable()
                .frame(width: 46.5, height: 46.5)
                .position(x: 710.25, y: 56.25)
            
            Text("""
Впечатляющий запас хода
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 100, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("Daimler CAC Regular", size: 32.5))
                .offset(x: 10, y: 167)
            
            Text("""
Из пункта А в пункт Б и далее – по вашему маршруту. Запас хода EQS – около 705 км. Мощность - до 385 кВт (524 л.с.)
Пополнить запас энергии на зарядной станции вы сможете всего лишь за 31 минуту
благодаря аккумуляторной батарее энергоемкостью 107,8 кВтч.
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 170, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("RobotoCondensed-Regular", size: 17.5))
                .offset(x: 10, y: 267)
        }
    }
}


struct Window1: View {
    private var images: [String] = ["3_6"]
    private var count: Int = 1
    
    var body: some View {
        ZStack {
            Rectangle()
                .ignoresSafeArea(.all)
                .frame(minWidth: 0, maxWidth: .infinity, minHeight: 0, maxHeight: .infinity, alignment: .center)
                .foregroundColor(Color("Color_Back"))
            Galery(images_list: images, images_count: count)
            Image("Logo")
                .resizable()
                .frame(width: 46.5, height: 46.5)
                .position(x: 710.25, y: 56.25)
            
            Text("""
Первый электрический седан класса люкс от Mercedes-EQ
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 100, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("Daimler CAC Regular", size: 32.5))
                .offset(x: 10, y: 167)
            
            Text("""
Электрический флагман

В новом EQS эмоциональность и интеллект, роскошь и цифровые технологии,
стиль, статус, комфорт и экологичность сливаются воедино.
Это первый электроседан класса люкс, лидер нового поколения автомобилей,
готовый подарить своему владельцу восхитительное чувство гармонии с собой
и окружающим миром.
""")
                .multilineTextAlignment(.leading)
                .frame(width: 700, height: 200, alignment: .topLeading)
                .foregroundColor(.white)
                .font(Font.custom("RobotoCondensed-Regular", size: 17.5))
                .offset(x: 10, y: 267)
        }
    }
}


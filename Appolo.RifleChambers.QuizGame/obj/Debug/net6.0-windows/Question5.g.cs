﻿#pragma checksum "..\..\..\Question5.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5A294BA4E6945DDF127C15130073335DA8272F4F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Appolo.RifleChambers.QuizGame;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Appolo.RifleChambers.QuizGame {
    
    
    /// <summary>
    /// Question5
    /// </summary>
    public partial class Question5 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 59 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_1;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock correct_button_text;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_2;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_3;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Definition;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Question5.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button next_page_button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Appolo.RifleChambers.QuizGame;component/question5.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Question5.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.button_1 = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\Question5.xaml"
            this.button_1.Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.correct_button_text = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.button_2 = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\Question5.xaml"
            this.button_2.Click += new System.Windows.RoutedEventHandler(this.Correct_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button_3 = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\..\Question5.xaml"
            this.button_3.Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Definition = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.next_page_button = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\Question5.xaml"
            this.next_page_button.Click += new System.Windows.RoutedEventHandler(this.Next_Page_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


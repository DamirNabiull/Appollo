﻿#pragma checksum "..\..\..\Question6.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AA8C85C44838043A21AD753ED342404E7779BB49"
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
    /// Question6
    /// </summary>
    public partial class Question6 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 180 "..\..\..\Question6.xaml"
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
            System.Uri resourceLocater = new System.Uri("/Appolo.RifleChambers.QuizGame;component/question6.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Question6.xaml"
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
            
            #line 74 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Correct_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 85 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Correct_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 96 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 110 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Correct_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 121 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 132 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 146 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Correct_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 157 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 168 "..\..\..\Question6.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Incorrect_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.next_page_button = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\..\Question6.xaml"
            this.next_page_button.Click += new System.Windows.RoutedEventHandler(this.Next_Page_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


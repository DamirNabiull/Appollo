﻿#pragma checksum "..\..\..\..\Email.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F84CBDDB245B211DE6709D93045CACCBA758C0A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Appolo.RifleChambers.Clerk;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Appolo.RifleChambers.Clerk {
    
    
    /// <summary>
    /// Email
    /// </summary>
    public partial class Email : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Email.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exit;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Email.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock For_Focus;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Email.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Email_Field;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Email.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Incorrect_Field;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Email.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Next;
        
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
            System.Uri resourceLocater = new System.Uri("/Appolo.RifleChambers.Clerk;component/email.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Email.xaml"
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
            
            #line 8 "..\..\..\..\Email.xaml"
            ((Appolo.RifleChambers.Clerk.Email)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Exit = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\Email.xaml"
            this.Exit.Click += new System.Windows.RoutedEventHandler(this.Exit_Button);
            
            #line default
            #line hidden
            return;
            case 3:
            this.For_Focus = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.Email_Field = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Incorrect_Field = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Next = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\..\..\Email.xaml"
            this.Next.Click += new System.Windows.RoutedEventHandler(this.Send_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 110 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 114 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 118 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 122 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 126 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 130 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 134 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 138 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 142 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 146 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 176 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 180 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 184 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 188 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 192 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 196 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 200 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 204 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 208 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 212 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 216 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 246 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 250 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 254 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 258 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 262 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 266 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 270 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 274 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 278 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 37:
            
            #line 282 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 38:
            
            #line 286 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Shift_Clcik);
            
            #line default
            #line hidden
            return;
            case 39:
            
            #line 314 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 318 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 322 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 326 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 330 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 334 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 338 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 342 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 346 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Clcik);
            
            #line default
            #line hidden
            return;
            case 48:
            
            #line 350 "..\..\..\..\Email.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Keyboard_Backspace_Clcik);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


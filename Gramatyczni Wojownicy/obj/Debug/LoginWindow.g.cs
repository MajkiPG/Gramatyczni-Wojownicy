﻿#pragma checksum "..\..\LoginWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AB77B86AD58712DE7D2C47A9363E3D4E195DC870"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Gramatyczni_Wojownicy;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace Gramatyczni_Wojownicy {
    
    
    /// <summary>
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Gramatyczni_Wojownicy.LoginWindow loginWindow;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox usersListBox;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newUserTextBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label logInButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label newUserButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label createUserButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Gramatyczni Wojownicy;component/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LoginWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.loginWindow = ((Gramatyczni_Wojownicy.LoginWindow)(target));
            return;
            case 2:
            this.usersListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 13 "..\..\LoginWindow.xaml"
            this.usersListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.UsersListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.newUserTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.logInButton = ((System.Windows.Controls.Label)(target));
            
            #line 16 "..\..\LoginWindow.xaml"
            this.logInButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LogInButton_Click);
            
            #line default
            #line hidden
            
            #line 16 "..\..\LoginWindow.xaml"
            this.logInButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\LoginWindow.xaml"
            this.logInButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.newUserButton = ((System.Windows.Controls.Label)(target));
            
            #line 17 "..\..\LoginWindow.xaml"
            this.newUserButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.NewUserButton_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\LoginWindow.xaml"
            this.newUserButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 17 "..\..\LoginWindow.xaml"
            this.newUserButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.createUserButton = ((System.Windows.Controls.Label)(target));
            
            #line 18 "..\..\LoginWindow.xaml"
            this.createUserButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CreateUserButton_MouseDown);
            
            #line default
            #line hidden
            
            #line 18 "..\..\LoginWindow.xaml"
            this.createUserButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.Button_MouseEnter);
            
            #line default
            #line hidden
            
            #line 18 "..\..\LoginWindow.xaml"
            this.createUserButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Button_MouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


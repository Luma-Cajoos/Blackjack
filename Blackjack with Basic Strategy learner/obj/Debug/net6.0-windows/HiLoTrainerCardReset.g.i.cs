﻿#pragma checksum "..\..\..\HiLoTrainerCardReset.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ACED617CBF8742D7EAC36B9590BB7DEBCB159C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Blackjack_with_Basic_Strategy_learner;
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


namespace Blackjack_with_Basic_Strategy_learner {
    
    
    /// <summary>
    /// HiLoTrainerCardReset
    /// </summary>
    public partial class HiLoTrainerCardReset : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimerText;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MistakesText;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TimerBestText;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MistakesBestText;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTNRestart;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\HiLoTrainerCardReset.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BTNMainMenu;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Blackjack with Basic Strategy learner;component/hilotrainercardreset.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\HiLoTrainerCardReset.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TimerText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.MistakesText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.TimerBestText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.MistakesBestText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.BTNRestart = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\HiLoTrainerCardReset.xaml"
            this.BTNRestart.Click += new System.Windows.RoutedEventHandler(this.BTNRestart_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BTNMainMenu = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\HiLoTrainerCardReset.xaml"
            this.BTNMainMenu.Click += new System.Windows.RoutedEventHandler(this.BTNMainMenu_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


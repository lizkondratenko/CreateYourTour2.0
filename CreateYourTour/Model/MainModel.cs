﻿using CreateYourTour.View;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateYourTour.Model
{
    static class MainModel 
    {
        private static EnterWindow enterWindow;
        private static bool enterWindowWasShowed = false;
        private static RegisterWindow registerWindow;
        private static bool registerWindowWasShowed = false;        

        public static void Enter_Click()
        {
            if (!enterWindowWasShowed)
            {
                enterWindowWasShowed = true;
                enterWindow = new EnterWindow();
                enterWindow.ShowDialog();
            }
            else
            {
                enterWindow = new EnterWindow();
                enterWindow.ShowDialog();
            }
        }

        public static void Register_Click()
        {   
            if (!registerWindowWasShowed)
            {
                registerWindowWasShowed = true;
                registerWindow = new RegisterWindow();
                registerWindow.ShowDialog();
            }
            else
            {
                registerWindow = new RegisterWindow();
                registerWindow.ShowDialog();
            }
        }

        public static void Close_Register_Window()
        {
            registerWindow.Close();
        }

        public static void Close_Enter_Window()
        {
            enterWindow.Close();
        }       
    }
}

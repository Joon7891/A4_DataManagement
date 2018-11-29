//Author: Joon Song
//Project Name: A4_DataManagement
//File Name: KeyboardHelper.cs
//Creation Date: 09/20/2018
//Modified Date: 09/20/2018
//Desription: Class to hold various subprograms to help with keyboard functionality

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace A4_DataManagement
{
    public static class KeyboardHelper
    {
        /// <summary>
        /// Subprogram to check if keystroke was a new one
        /// </summary>
        /// <param name="key">The key to check for new keystroke</param>
        /// <returns>Whether or whether not a keystroke is a new one</returns>
        public static bool NewKeyStroke(Keys key)
        {
            //Returning true of keystroke is detected to be a new one
            if (Main.NewKeyboard.IsKeyDown(key) && !Main.OldKeyboard.IsKeyDown(key))
            {
                return true;
            }

            //Otherwise returning false
            return false;
        }

        /// <summary>
        /// Subprogram to build a string using keystrokes
        /// </summary>
        /// <param name="stringToBuild">The string to build</param>
        /// <param name="maxLength">The max length of the string</param>
        public static void BuildString(ref string stringToBuild, int maxLength)
        {
            //Continuing to build string if max length is not reached and new keystroke is made
            if (stringToBuild.Length < maxLength)
            {
                for (Keys key = Keys.A; key <= Keys.Z; key++)
                {
                    if (NewKeyStroke(key))
                    {
                        stringToBuild += key.ToString();
                    }
                }
            }

            //Removing last character if backspace is pressed and string is long enough
            if (stringToBuild.Length > 1 && NewKeyStroke(Keys.Back))
            {
                stringToBuild = stringToBuild.Substring(0, stringToBuild.Length - 1);
            }
        }
    }
}
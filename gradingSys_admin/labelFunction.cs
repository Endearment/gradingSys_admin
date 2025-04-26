using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GradingSys_SIA
{
    public static class labelFunction
    {
        public static void Label_MouseEnter(object? sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                if (lbl.Name == "label4")
                {
                    lbl.BackColor = Color.FromArgb(128, 255, 0, 0);
                }
                else
                {
                    lbl.BackColor = Color.FromArgb(150, 150, 150, 100);
                }
            }
        }

        public static void Label_MouseLeave(object? sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                lbl.BackColor = Color.Transparent;

            }
        }
    }
}

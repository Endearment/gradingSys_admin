using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gradingSys_admin
{

    public static class FormHelper
    {
        public static void ShowDialogWithBackdrop(Form ownerForm, Form childForm)
        {
            Form formBackground = new Form();
            try
            {
                Rectangle screenBounds = ownerForm.Bounds;
                Point screenLocation = ownerForm.PointToScreen(Point.Empty);

                formBackground.StartPosition = FormStartPosition.Manual;
                formBackground.FormBorderStyle = FormBorderStyle.None;
                formBackground.Opacity = 0.70d;
                formBackground.BackColor = Color.Black;
                formBackground.Size = screenBounds.Size;
                formBackground.Location = screenLocation;
                formBackground.TopMost = true;
                formBackground.ShowInTaskbar = false;
                formBackground.Show();

                childForm.StartPosition = FormStartPosition.Manual;
                childForm.Location = new Point(
                    formBackground.Left + (formBackground.Width - childForm.Width) / 2,
                    formBackground.Top + (formBackground.Height - childForm.Height) / 2
                );

                childForm.Owner = formBackground;
                childForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                formBackground.Dispose();
            }
        }
        public static Form? GetTopMostForm(Control control)
        {
            Control? parent = control;
            while (parent?.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent as Form;
        }
    }
}

using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        using (Document doc = new Document())
        {
            // Add a page that will host the button and the floating box
            Page page = doc.Pages.Add();

            // Use PopupAnnotation as the floating box (shown on mouse entry)
            PopupAnnotation floatingBox = new PopupAnnotation(
                page,
                new Aspose.Pdf.Rectangle(100, 500, 300, 550) // llx, lly, urx, ury
            );
            floatingBox.Name = "MyFloatingBox";                     // Identifier used by HideAction
            floatingBox.Contents = "Info box displayed on hover.";   // Text shown inside the popup
            page.Annotations.Add(floatingBox);

            // Create a button field (ButtonField is the correct type in Aspose.Pdf.Forms)
            ButtonField button = new ButtonField(
                page,
                new Aspose.Pdf.Rectangle(50, 600, 150, 650) // llx, lly, urx, ury
            );
            button.PartialName = "ShowBoxButton";
            // DefaultAppearance must be set before assigning a caption
            button.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            button.NormalCaption = "Hover me";

            // Assign a HideAction to the button's OnEnter event.
            // Hide = false means the specified annotation(s) will be shown.
            // The HideAction constructor expects the target annotation instance and a bool flag.
            button.Actions.OnEnter = new HideAction(floatingBox, false);

            // Add the button to the document's form collection (page index is 1‑based)
            doc.Form.Add(button, 1);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}

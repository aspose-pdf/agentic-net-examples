using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define rectangle for the floating box (acts as a hidden text field)
            Aspose.Pdf.Rectangle boxRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a hidden TextBoxField that will serve as the floating box
            TextBoxField floatingBox = new TextBoxField(page, boxRect);
            floatingBox.PartialName = "FloatingBox";
            floatingBox.Value = "Hidden content displayed on hover.";
            floatingBox.ReadOnly = true;
            // Set the hidden flag so it is not visible initially
            floatingBox.Flags |= AnnotationFlags.Hidden;
            // Optional visual styling
            floatingBox.Border = new Border(floatingBox) { Width = 1 };
            floatingBox.Color = Aspose.Pdf.Color.LightGray; // border / background color for the field
            // Add the floating box to the form
            doc.Form.Add(floatingBox, 1);

            // Create a transparent button overlaying the same area
            ButtonField button = new ButtonField(page, boxRect);
            button.PartialName = "HoverButton";
            button.Border = new Border(button) { Width = 0 }; // No visible border
            button.Color = Aspose.Pdf.Color.Transparent;   // Fully transparent
            // Assign HideAction to the button's OnExit event to hide the floating box
            button.Actions.OnExit = new HideAction(floatingBox);
            // Optionally, assign a ShowAction on Enter (not required by the task)
            // button.Actions.OnEnter = new ShowAction(floatingBox);
            // Add the button to the form
            doc.Form.Add(button, 1);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with interactive floating box saved to '{outputPath}'.");
    }
}

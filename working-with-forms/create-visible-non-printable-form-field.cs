using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "field_visible_not_printable.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle that will contain the field (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create the text box field on the page (constructor: Page, Rectangle)
            TextBoxField txtField = new TextBoxField(page, rect);
            txtField.PartialName = "SampleField"; // set the field name

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Make the field visible (clear Hidden/Invisible) and NOT printable (clear Print flag)
            txtField.Flags &= ~AnnotationFlags.Print;
            txtField.Flags &= ~AnnotationFlags.Hidden;
            txtField.Flags &= ~AnnotationFlags.Invisible;

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

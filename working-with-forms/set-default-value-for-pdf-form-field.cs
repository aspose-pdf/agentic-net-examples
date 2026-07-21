using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;   // DefaultAppearance resides here
using Aspose.Pdf.Drawing;      // Rectangle for positioning

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_default.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required before placing a field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a text box field on the page
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",          // internal name of the field
                Value       = "Default Text",         // default value shown when PDF opens
                // Set default appearance (font, size, color) for the field's text
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
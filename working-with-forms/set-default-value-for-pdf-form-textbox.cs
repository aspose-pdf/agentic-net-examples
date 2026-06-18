using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for annotation types (if needed elsewhere)
using Aspose.Pdf.Forms; // for form field classes

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_default.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required before adding a field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the page
            TextBoxField textField = new TextBoxField(page, rect)
            {
                // Set the default value that will be visible when the PDF opens
                Value = "Enter your name here",

                // Define the default appearance (font, size, color)
                // Use System.Drawing.Color for the color argument
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

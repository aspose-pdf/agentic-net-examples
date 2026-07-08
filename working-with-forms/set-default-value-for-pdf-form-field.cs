using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_default.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the field rectangle (left, bottom, right, top)
            // Fully qualify to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "SampleField",          // field identifier
                Value = "Enter your name"             // default value shown when PDF opens
            };

            // Set the default appearance (font, size, color) for the field
            // Note: DefaultAppearance(string, double, System.Drawing.Color) constructor is required
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the PDF (Document.Save writes a PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
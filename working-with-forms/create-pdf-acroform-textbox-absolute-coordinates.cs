using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Define the absolute position of the field:
            // lower‑left (100, 600), upper‑right (300, 620) → width 200, height 20
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field at the specified rectangle
            TextBoxField txtField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "NameField",
                Value = "John Doe"
            };

            // Set default appearance (font, size, color) using the proper constructor
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Add the field to the form on page 1
            doc.Form.Add(txtField, 1);

            // Save the PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}
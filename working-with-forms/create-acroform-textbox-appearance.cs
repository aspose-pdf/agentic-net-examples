using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Retained for completeness; not used directly in this example

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "AcroForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field on the document
            // Constructor (Document, Rectangle) places the field on the specified page
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                // Set the field name (used to retrieve the field later)
                PartialName = "SampleField"
            };

            // Set the default appearance: font, size, and text color
            // Use the constructor that accepts font name, size, and System.Drawing.Color
            textField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Blue);

            // Set the border width (Border requires the parent annotation in its constructor)
            textField.Border = new Border(textField) { Width = 1 };

            // Set the border color via the field's own Color property (Border has no Color member)
            textField.Color = Aspose.Pdf.Color.Black;

            // Add the field to the form on page 1 (1‑based page indexing)
            doc.Form.Add(textField, 1);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}

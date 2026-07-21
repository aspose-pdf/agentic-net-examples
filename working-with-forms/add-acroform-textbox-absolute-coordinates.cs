using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "AcroFormAbsolute.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the field will be placed.
            // Rectangle(left, bottom, right, top) – all values are in points.
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text box field. The constructor (Document, Rectangle) places the field on the given page.
            TextBoxField textField = new TextBoxField(doc, fieldRect)
            {
                // Set a unique name for the field (used when accessing it later)
                Name = "SampleTextField",
                // Optional: set an initial value
                Value = "Enter text here",
                // Optional: make the field visible and editable
                ReadOnly = false,
                Required = false
            };

            // Add the field to the form on page 1.
            // The Add method also accepts a page number (1‑based) for placement.
            doc.Form.Add(textField, 1);

            // If you need an additional appearance (e.g., the same field on another page or location),
            // you can use AddFieldAppearance. Here we demonstrate adding a second appearance on the same page.
            // Define a second rectangle for the additional appearance.
            Aspose.Pdf.Rectangle secondRect = new Aspose.Pdf.Rectangle(350, 500, 550, 550);
            doc.Form.AddFieldAppearance(textField, 1, secondRect);

            // Save the PDF. The Save(string) overload always writes PDF regardless of the file extension.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm fields saved to '{outputPath}'.");
    }
}
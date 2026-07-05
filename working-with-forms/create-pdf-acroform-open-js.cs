using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithOpenJS.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field (left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a simple text field and associate it with the page
            Field textField = new Field(doc)
            {
                Name = "SampleText",          // Full field name
                PartialName = "SampleText",   // Partial name (same as full for simple fields)
                Rect = fieldRect,               // Position on the page
                Value = "Enter text here"      // Default value
            };

            // Associate the field with the page using the Form.Add overload that expects a page number (1‑based)
            doc.Form.Add(textField, 1);

            // Set a document‑level JavaScript that runs when the PDF is opened
            JavascriptAction openJs = new JavascriptAction("app.alert('Document opened');");
            doc.OpenAction = openJs;

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm and open‑action JavaScript saved to '{outputPath}'.");
    }
}

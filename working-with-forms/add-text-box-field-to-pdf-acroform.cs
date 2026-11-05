using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTextField.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and dispose it properly
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text field (llx, lly, urx, ury)
            Rectangle fieldRect = new Rectangle(100, 700, 300, 730);

            // Create a text box field using the page‑and‑rectangle constructor
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextField",   // field name
                Value = "Enter text here",         // default value
                MaxLen = 100,                       // character limit (correct property name)
                Multiline = false                   // single‑line field (correct property name)
            };

            // Add the field to the form, specifying the page number (1‑based)
            doc.Form.Add(textField, page.Number);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm saved to '{outputPath}'.");
    }
}

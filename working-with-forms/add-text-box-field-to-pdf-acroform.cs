using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border and BorderStyle

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithTextField.pdf";

        // Ensure the output directory exists (Path.GetDirectoryName returns null for a file in the current directory)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text field will appear
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box field (AcroForm field) on the first page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleTextField", // internal name of the field
                Value = "Enter text here"        // default value displayed in the field
            };

            // Set a simple border for the field (Border requires the parent annotation in the constructor)
            textField.Border = new Border(textField)
            {
                Width = 1,
                Style = BorderStyle.Solid
            };
            // Optional: set border color (the Color property belongs to the annotation itself)
            textField.Color = Color.Black;

            // Add the field to the document's form (AcroForm) and associate it with page 1
            doc.Form.Add(textField, 1);

            // Save the PDF; Document.Save(string) writes PDF regardless of extension
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with AcroForm created at '{outputPath}'.");
    }
}

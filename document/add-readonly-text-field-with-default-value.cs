using System;
using System.IO;
using System.Drawing;                     // Required for DefaultAppearance color
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;            // DefaultAppearance resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_textfield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the field will be placed (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the field rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextBoxField on the selected page
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                // Internal name of the field
                PartialName = "SampleTextField",

                // Default value displayed in the field
                Value = "Default Value",

                // Make the field read‑only
                ReadOnly = true,

                // Set default appearance (font, size, color)
                // DefaultAppearance constructor requires System.Drawing.Color
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the field to the page's annotation collection
            page.Annotations.Add(textField);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only text field: {outputPath}");
    }
}
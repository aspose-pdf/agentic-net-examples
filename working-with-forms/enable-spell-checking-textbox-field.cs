using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_spellcheck.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the text box will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a new text box field on the page
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "MyTextField", // field name
                Value = ""                    // optional default value
            };

            // Enable spell checking for this field
            txtField.SpellCheck = true;

            // Add the field to the document's form collection
            doc.Form.Add(txtField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with spell checking enabled: {outputPath}");
    }
}
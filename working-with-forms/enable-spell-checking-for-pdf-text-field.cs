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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the text field will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a new text box field on the page
            TextBoxField textField = new TextBoxField(page, rect)
            {
                PartialName = "MyTextField",   // field name
                Contents = "Enter text here",  // default visible text
                SpellCheck = true              // enable spell checking
            };

            // Add the field to the document's form collection
            doc.Form.Add(textField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with spell checking enabled: {outputPath}");
    }
}
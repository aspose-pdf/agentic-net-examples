using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API
        using (Document doc = new Document(inputPath))
        {
            // Check that the document contains an AcroForm
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // Example modification: set the value of a text box field named "Name"
                // The Form indexer returns a generic Field; cast to the appropriate type
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }

                // Additional form modifications can be performed here,
                // e.g., checking a checkbox, selecting a list item, etc.
            }

            // Save the modified PDF to the output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields found in the document.");
            }
            else
            {
                // LINQ query: retrieve only TextBoxField instances
                var textBoxFields = doc.Form.Fields
                    .OfType<TextBoxField>()
                    .ToList();

                Console.WriteLine($"Found {textBoxFields.Count} text box field(s).");

                // Example operation: set a default value for each text box
                foreach (var tb in textBoxFields)
                {
                    tb.Value = "Sample text";
                }
            }

            // Save the (potentially) modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
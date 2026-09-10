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
        const string outputPath = "textboxes.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Check if the document contains any form fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No form fields present in the document.");
                return;
            }

            // Use LINQ to filter only TextBoxField instances
            var textBoxFields = doc.Form.Fields
                .OfType<TextBoxField>()
                .ToList();

            // Write the names and current values of the text box fields to a text file
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                foreach (var field in textBoxFields)
                {
                    writer.WriteLine($"Name: {field.FullName}");
                    writer.WriteLine($"Value: {field.Value}");
                    writer.WriteLine();
                }
            }

            Console.WriteLine($"Extracted {textBoxFields.Count} text box fields to '{outputPath}'.");
        }
    }
}
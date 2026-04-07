using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Lock each form field by setting it to read‑only
            foreach (var field in doc.Form)
            {
                field.ReadOnly = true;
            }

            // Alternatively, you could flatten the entire form:
            // doc.Form.Flatten();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Locked PDF saved to '{outputPath}'.");
    }
}
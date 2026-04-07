using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "plain_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, flatten it (remove all form fields), and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Flatten removes all interactive form fields and places their current values on the page.
            doc.Flatten();

            // Save the flattened PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Plain PDF saved to '{outputPath}'.");
    }
}
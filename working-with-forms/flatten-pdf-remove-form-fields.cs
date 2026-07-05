using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "flattened.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, flatten it (remove all form fields), and save the result.
        using (Document doc = new Document(inputPath))
        {
            doc.Flatten();               // Removes all form fields, keeping their values.
            doc.Save(outputPath);        // Save as a plain, non‑interactive PDF.
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
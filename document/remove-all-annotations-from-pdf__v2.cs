using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Remove all annotations from each page
            foreach (Page page in doc.Pages)
            {
                page.Annotations.Clear();
            }

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all embedded files in the document
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Example: output the name of each embedded file
                Console.WriteLine($"Embedded file: {fileSpec.Name}");
                // Additional processing can be done here (e.g., extract contents)
            }

            // Save the (unchanged) document to a new PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Loop through all embedded files (attachments)
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Example: output the name of each attachment
                Console.WriteLine($"Embedded file: {fileSpec.Name}");
            }

            // Save the (unchanged) document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
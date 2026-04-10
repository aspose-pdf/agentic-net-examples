using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_zugferd.pdf";
        const string outputPath = "output_zugferd_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing ZUGFeRD PDF, modify metadata, and save.
        using (Document doc = new Document(inputPath))
        {
            // Add custom metadata entries.
            // The Metadata property implements IDictionary<string, XmpValue> and
            // provides an Add(string, object) overload for simple values.
            doc.Metadata.Add("ProjectCode", "PRJ-2023-001");
            doc.Metadata.Add("Department", "Finance");

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
    }
}
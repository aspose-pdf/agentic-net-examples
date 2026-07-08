using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "formdata.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Export all AcroForm data (annotations) to XFDF (XML) format
            doc.ExportAnnotationsToXfdf(outputPath);
        }

        Console.WriteLine($"Form data exported to '{outputPath}'.");
    }
}
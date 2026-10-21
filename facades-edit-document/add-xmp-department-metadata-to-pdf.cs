using System;
using System.IO;
using Aspose.Pdf;

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
        Document pdf = new Document(inputPath);

        // Register the PDF namespace (http://ns.adobe.com/pdf/1.3/) with a prefix
        pdf.Metadata.RegisterNamespaceUri("pdf", "http://ns.adobe.com/pdf/1.3/");

        // Add or replace the custom XMP property Department with value Finance
        pdf.Metadata["pdf:Department"] = "Finance";

        // Save the updated PDF
        pdf.Save(outputPath);

        Console.WriteLine($"XMP metadata 'Department=Finance' added. Output saved to '{outputPath}'.");
    }
}

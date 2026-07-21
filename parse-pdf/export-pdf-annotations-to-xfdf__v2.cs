using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a file stream for the XFDF output
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including form field data) to XFDF
                doc.ExportAnnotationsToXfdf(xfdfStream);
                // Stream will be closed automatically by the using block
            }

            // Optionally save the document if further modifications were made
            // doc.Save("modified.pdf");
        }

        Console.WriteLine($"XFDF data exported to '{outputXfdfPath}'.");
    }
}
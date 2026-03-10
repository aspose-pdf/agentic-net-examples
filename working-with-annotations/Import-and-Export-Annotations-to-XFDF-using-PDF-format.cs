using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF and export its annotations to an XFDF file
            using (Document sourceDoc = new Document(inputPdfPath))
            {
                sourceDoc.ExportAnnotationsToXfdf(xfdfPath);
                Console.WriteLine($"Annotations exported to '{xfdfPath}'.");
            }

            // Load the PDF again (or another PDF) and import the previously exported annotations
            using (Document targetDoc = new Document(inputPdfPath))
            {
                targetDoc.ImportAnnotationsFromXfdf(xfdfPath);
                targetDoc.Save(outputPdfPath);
                Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
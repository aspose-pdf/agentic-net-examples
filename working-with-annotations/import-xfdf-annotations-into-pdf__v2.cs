using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, XFDF file containing annotations, and the output PDF
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Verify that the required files exist before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"Error: XFDF file not found at '{xfdfPath}'.");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal
        using (Document doc = new Document(pdfPath))
        {
            // Import all annotations from the XFDF file into the loaded document.
            // This operation preserves the existing page layout and content.
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the modified document to the specified output path.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Successfully imported annotations and saved to '{outputPath}'.");
    }
}
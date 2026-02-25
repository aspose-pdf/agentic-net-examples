using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputCgmPath}");
            return;
        }

        // Create default load options for CGM import
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Load the CGM file into a PDF Document and save it as a regular PDF
        using (Document pdfDoc = new Document())
        {
            // LoadFrom converts the CGM input to PDF pages
            pdfDoc.LoadFrom(inputCgmPath, loadOptions);

            // If the source were a PDF/A and you needed to strip compliance, you could call:
            // pdfDoc.RemovePdfaCompliance();

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed: '{outputPdfPath}'");
    }
}
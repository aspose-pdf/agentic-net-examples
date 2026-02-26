using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Provides XfdfReader if needed

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // Source PDF
        const string xfdfPath = "annotations.xfdf"; // XFDF (XML) file with annotations
        const string outputPath = "output.pdf";      // Resulting PDF

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Import annotations from the XFDF (XML) file into the PDF
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}
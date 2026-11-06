using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as requested

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the updated PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
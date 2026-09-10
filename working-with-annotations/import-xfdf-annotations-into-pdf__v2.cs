using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
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
            // Load the existing PDF document
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the XFDF file
                doc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the PDF with the imported annotations, preserving layout and content
                doc.Save(outputPath);
            }

            Console.WriteLine($"Annotations imported successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
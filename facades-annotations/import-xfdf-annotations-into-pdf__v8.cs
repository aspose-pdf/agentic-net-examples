using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath    = "input.pdf";
        const string xfdfPath   = "annotations.xfdf";
        const string outputPath = "output.pdf";

        // Verify that source files exist.
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

        // Load the PDF document using the high‑level Document API.
        Document doc = new Document(pdfPath);

        // Import annotations from the XFDF file.
        // Document provides ImportAnnotationsFromXfdf (path or stream).
        using (FileStream xfdfStream = File.OpenRead(xfdfPath))
        {
            doc.ImportAnnotationsFromXfdf(xfdfStream);
        }

        // Save the updated PDF with the imported annotations.
        doc.Save(outputPath);

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}

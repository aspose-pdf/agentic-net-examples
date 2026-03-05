using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file using default load options
        using (Document doc = new Document())
        {
            CgmLoadOptions loadOptions = new CgmLoadOptions(); // default A4 300dpi page size
            doc.LoadFrom(inputCgmPath, loadOptions);

            // Save the loaded document as a regular PDF (non‑PDF/A)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"CGM file successfully converted to PDF: {outputPdfPath}");
    }
}
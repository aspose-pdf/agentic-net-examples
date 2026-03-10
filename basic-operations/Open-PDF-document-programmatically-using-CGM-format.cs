using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Load options for CGM conversion (default A4 page size, 300 DPI)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Open the CGM file and convert it to a PDF document
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM file converted to PDF: {pdfPath}");
    }
}
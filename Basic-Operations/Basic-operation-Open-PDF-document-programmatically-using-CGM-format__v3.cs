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

        // Load the CGM file with default options (A4 page size, 300 dpi)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Document implements IDisposable – wrap in using for deterministic cleanup
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the converted document as PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM successfully converted to PDF: '{pdfPath}'");
    }
}
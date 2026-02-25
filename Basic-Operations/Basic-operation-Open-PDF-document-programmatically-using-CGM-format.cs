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

        // Load the CGM file with default load options (A4 page size, 300 dpi)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the resulting PDF; no SaveOptions needed because PDF is the default format
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM successfully converted to PDF: '{pdfPath}'");
    }
}
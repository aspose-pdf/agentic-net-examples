using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Create default load options for CGM → PDF conversion (A4 page size, 300 dpi)
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Open the CGM file using the load options; the constructor performs the conversion
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the resulting PDF document
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM file successfully converted to PDF: {pdfPath}");
    }
}
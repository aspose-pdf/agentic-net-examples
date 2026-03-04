using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input CGM file and desired PDF output
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify the CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Create default load options for CGM conversion
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Open the CGM file with the load options; the constructor converts it to a PDF document
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the resulting PDF
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM file successfully converted to PDF: {pdfPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document and CgmLoadOptions

class Program
{
    static void Main()
    {
        // Input CGM file and desired PDF output path
        const string cgmPath = "input.cgm";
        const string pdfPath = "output.pdf";

        // Verify that the CGM file exists before attempting to load it
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"File not found: {cgmPath}");
            return;
        }

        // Create default load options for CGM conversion
        CgmLoadOptions loadOptions = new CgmLoadOptions();

        // Open the CGM file using the load options; this converts it to a PDF document in memory
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Save the resulting PDF document to the specified output file
            doc.Save(pdfPath);
        }

        Console.WriteLine($"CGM file successfully converted to PDF: {pdfPath}");
    }
}
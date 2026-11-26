using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace – load‑option classes (e.g., RtfLoadOptions) are defined here

class Program
{
    static void Main()
    {
        const string rtfPath = "input.rtf";
        const string pdfPath = "output.pdf";

        if (!File.Exists(rtfPath))
        {
            Console.Error.WriteLine($"RTF file not found: {rtfPath}");
            return;
        }

        // Configure RTF loading options (default options are sufficient for most cases)
        RtfLoadOptions loadOptions = new RtfLoadOptions();

        // Load the RTF document and automatically convert it to PDF in memory
        using (Document pdfDoc = new Document(rtfPath, loadOptions))
        {
            // Save the in‑memory PDF representation to a file
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"RTF successfully converted to PDF: {pdfPath}");
    }
}

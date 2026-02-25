using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, CgmLoadOptions, PdfFormat, etc.)

class Program
{
    static void Main()
    {
        const string cgmPath   = "input.cgm";   // Source CGM file (input‑only format)
        const string pdfPath   = "output.pdf";  // Desired PDF output (standard PDF, not PDF/A)

        // Verify source file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"Source file not found: {cgmPath}");
            return;
        }

        try
        {
            // Load the CGM file into a Document. The resulting Document is a PDF representation.
            using (Document doc = new Document())
            {
                // CgmLoadOptions provides default page size (A4 @ 300 dpi). Adjust if needed.
                CgmLoadOptions loadOptions = new CgmLoadOptions();
                doc.LoadFrom(cgmPath, loadOptions);

                // At this point the document may be PDF/A compliant (depends on the CGM conversion defaults).
                // To ensure a plain PDF output, simply save the document without specifying any special format.
                doc.Save(pdfPath);   // Saves as standard PDF (PDF 1.7 by default)

                Console.WriteLine($"CGM successfully converted to PDF: {pdfPath}");
            }
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing (e.g., malformed CGM)
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Generic error handling
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
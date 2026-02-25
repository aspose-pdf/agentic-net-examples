using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, EpubLoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Input EPUB file (could be a PDF/A document exported as EPUB)
        const string epubPath   = "input.epub";
        // Desired output PDF file (standard PDF, not PDF/A)
        const string pdfPath    = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(epubPath))
        {
            Console.Error.WriteLine($"Source file not found: {epubPath}");
            return;
        }

        try
        {
            // Load the EPUB using the dedicated load options.
            // The constructor of Document that accepts a file name and LoadOptions
            // performs the conversion from EPUB to an in‑memory PDF representation.
            using (Document doc = new Document(epubPath, new EpubLoadOptions()))
            {
                // Save the document as a regular PDF.
                // No SaveOptions are required because Save(string) defaults to PDF format.
                doc.Save(pdfPath);
            }

            Console.WriteLine($"EPUB successfully converted to PDF: '{pdfPath}'");
        }
        catch (Exception ex)
        {
            // Catch any conversion‑related errors (e.g., unsupported content, I/O issues)
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
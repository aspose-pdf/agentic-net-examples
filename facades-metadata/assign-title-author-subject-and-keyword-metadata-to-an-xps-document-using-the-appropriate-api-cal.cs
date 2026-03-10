using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facade API for metadata manipulation
using Aspose.Pdf;           // Core API for document conversion
using Aspose.Pdf;           // XpsSaveOptions is in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file (source)
        const string inputPdfPath = "input.pdf";

        // Temporary PDF file that will contain the updated metadata
        const string tempPdfPath = "temp_with_metadata.pdf";

        // Final XPS output file
        const string outputXpsPath = "output.xps";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the PDF via PdfFileInfo (Facades) and set metadata
        // ------------------------------------------------------------
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
        {
            // Assign required metadata fields
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of XPS metadata assignment";
            pdfInfo.Keywords = "Aspose, PDF, XPS, Metadata";

            // Save the PDF with the new metadata (preserves XMP as well)
            // SaveNewInfo writes only the changed properties, leaving others intact
            pdfInfo.SaveNewInfo(tempPdfPath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the updated PDF and convert it to XPS
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document(tempPdfPath))
        {
            // XPS conversion requires explicit save options
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS
            pdfDoc.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"XPS file created with metadata: {outputXpsPath}");
    }
}
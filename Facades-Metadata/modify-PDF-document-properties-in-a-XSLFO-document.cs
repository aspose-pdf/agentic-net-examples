using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory that contains the XSL‑FO source file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the XSL‑FO file that will be converted to PDF.
        string xslFoPath = Path.Combine(dataDir, "sample.xslfo");

        // Path where the resulting PDF with updated properties will be saved.
        string outputPdfPath = Path.Combine(dataDir, "sample_modified.pdf");

        // Verify that the source XSL‑FO file exists.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // Load the XSL‑FO file into a PDF document using XslFoLoadOptions.
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();   // no external XSL required
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // -----------------------------------------------------------------
            // Modify PDF document properties (metadata) using the Facades API.
            // PdfFileInfo works on an existing Document instance and allows
            // setting standard metadata fields such as Title, Author, etc.
            // -----------------------------------------------------------------
            PdfFileInfo fileInfo = new PdfFileInfo(pdfDoc);

            fileInfo.Title    = "Converted PDF from XSL‑FO";
            fileInfo.Author   = "John Doe";
            fileInfo.Subject  = "Demonstration of metadata modification";
            fileInfo.Keywords = "Aspose.Pdf; XSL‑FO; Metadata";

            // Save the PDF with the updated metadata.
            // SaveNewInfo writes the modified information to a new file while
            // preserving the original PDF content.
            bool saved = fileInfo.SaveNewInfo(outputPdfPath);
            if (saved)
                Console.WriteLine($"PDF saved successfully to '{outputPdfPath}'.");
            else
                Console.Error.WriteLine("Failed to save the PDF with new metadata.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting XML file
        const string pdfPath = "input.pdf";
        const string xmlPath = "output.xml";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Set PDF metadata (Title, Author, Subject, Keywords) using
        //    the PdfFileInfo facade from Aspose.Pdf.Facades.
        // -----------------------------------------------------------------
        // The facade works on a file path; it does not need a separate using
        // block for disposal because it implements IDisposable.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(pdfPath))
        {
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of metadata setting";
            pdfInfo.Keywords = "Aspose;PDF;Metadata;Example";

            // Save the updated information to a temporary PDF file.
            // SaveNewInfoWithXmp preserves existing XMP metadata while
            // writing the new values.
            string tempPdfPath = Path.Combine(Path.GetDirectoryName(pdfPath) ?? ".", "temp_updated.pdf");
            bool saved = pdfInfo.SaveNewInfoWithXmp(tempPdfPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save updated PDF metadata.");
                return;
            }

            // -----------------------------------------------------------------
            // 2. Load the updated PDF and export its structure to an XML file.
            // -----------------------------------------------------------------
            using (Document doc = new Document(tempPdfPath))
            {
                // XmlSaveOptions is the correct class for exporting to XML.
                XmlSaveOptions xmlOpts = new XmlSaveOptions();
                doc.Save(xmlPath, xmlOpts);
            }

            // Clean up the temporary PDF file.
            try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
        }

        Console.WriteLine($"PDF metadata set and exported to XML: {xmlPath}");
    }
}
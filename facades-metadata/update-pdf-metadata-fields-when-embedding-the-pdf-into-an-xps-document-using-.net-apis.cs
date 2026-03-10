using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the source PDF
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // 2. Update PDF metadata using the Facade API (PdfFileInfo)
            // -----------------------------------------------------------------
            using (PdfFileInfo metaInfo = new PdfFileInfo(pdfDoc))
            {
                // Set desired metadata fields
                metaInfo.Title    = "Updated Title";
                metaInfo.Author   = "John Doe";
                metaInfo.Subject  = "Sample Subject";
                metaInfo.Keywords = "Aspose, PDF, XPS";

                // Persist the changes back to a temporary PDF file.
                // SaveNewInfoWithXmp updates the file information while preserving existing XMP data.
                string tempPdfPath = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? ".", "temp_updated.pdf");
                bool saved = metaInfo.SaveNewInfoWithXmp(tempPdfPath);
                if (!saved)
                {
                    Console.Error.WriteLine("Failed to save updated PDF metadata.");
                    return;
                }

                // -----------------------------------------------------------------
                // 3. Convert the updated PDF to XPS using XpsSaveOptions
                // -----------------------------------------------------------------
                using (Document updatedDoc = new Document(tempPdfPath))
                {
                    XpsSaveOptions xpsOptions = new XpsSaveOptions(); // default options
                    updatedDoc.Save(outputXpsPath, xpsOptions);      // explicit SaveOptions as required
                }

                // Clean up the temporary PDF file
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }

        Console.WriteLine($"PDF metadata updated and embedded into XPS: {outputXpsPath}");
    }
}
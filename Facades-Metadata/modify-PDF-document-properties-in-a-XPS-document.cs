using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF and desired XPS output paths
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Temporary PDF file that will hold the updated metadata
        string tempPdfPath = Path.Combine(Path.GetDirectoryName(inputPdfPath) ?? "", "temp_modified.pdf");

        try
        {
            // ------------------------------------------------------------
            // Step 1: Modify PDF document properties using the Facades API
            // ------------------------------------------------------------
            using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdfPath))
            {
                // Set desired metadata properties
                pdfInfo.Title   = "Updated Document Title";
                pdfInfo.Author  = "John Doe";
                pdfInfo.Subject = "Sample Subject";
                pdfInfo.Keywords = "Aspose, PDF, XPS, Metadata";

                // Save the PDF with the new metadata to a temporary file
                // SaveNewInfo returns true on success; we ignore the return value here
                pdfInfo.SaveNewInfo(tempPdfPath);
            }

            // ------------------------------------------------------------
            // Step 2: Convert the updated PDF to XPS format
            // ------------------------------------------------------------
            using (Document pdfDoc = new Document(tempPdfPath))
            {
                // Initialize XPS save options (no special settings required)
                XpsSaveOptions xpsOptions = new XpsSaveOptions();

                // Save the document as XPS
                pdfDoc.Save(outputXpsPath, xpsOptions);
            }

            Console.WriteLine($"PDF properties updated and saved as XPS: '{outputXpsPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF file if it exists
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
            }
        }
    }
}
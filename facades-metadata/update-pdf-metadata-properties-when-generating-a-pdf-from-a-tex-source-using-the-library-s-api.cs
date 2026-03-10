using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texFilePath   = "input.tex";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(texFilePath))
        {
            Console.Error.WriteLine($"TeX source not found: {texFilePath}");
            return;
        }

        // Load the TeX source and convert it to a PDF document
        using (Document pdfDoc = new Document(texFilePath, new TeXLoadOptions()))
        {
            // ---- Update standard metadata via DocumentInfo ----
            pdfDoc.Info.Title   = "Generated PDF from TeX";
            pdfDoc.Info.Author  = "John Doe";
            pdfDoc.Info.Subject = "Demonstration of TeX → PDF conversion";
            pdfDoc.Info.Keywords = "Aspose.Pdf, TeX, Metadata";

            // ---- Update custom metadata via PdfFileInfo (Facades API) ----
            using (PdfFileInfo fileInfo = new PdfFileInfo(pdfDoc))
            {
                // Set any custom key/value pairs
                fileInfo.SetMetaInfo("Project", "AsposeDemo");
                fileInfo.SetMetaInfo("Version", "1.0");
                fileInfo.SetMetaInfo("GeneratedOn", DateTime.UtcNow.ToString("u"));

                // Save the PDF with the updated metadata
                // SaveNewInfo writes a new file preserving the original content
                bool success = fileInfo.SaveNewInfo(outputPdfPath);
                Console.WriteLine(success
                    ? $"PDF saved with updated metadata to '{outputPdfPath}'."
                    : "Failed to save PDF with updated metadata.");
            }
        }
    }
}
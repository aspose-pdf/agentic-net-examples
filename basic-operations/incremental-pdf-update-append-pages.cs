using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the original PDF, an optional PDF to append, and the final output.
        const string originalPdfPath = "input.pdf";
        const string pdfToAppendPath = "newpage.pdf";
        const string outputPdfPath   = "output.pdf";

        // Verify the original PDF exists.
        if (!File.Exists(originalPdfPath))
        {
            Console.Error.WriteLine($"File not found: {originalPdfPath}");
            return;
        }

        // Open the original PDF with a read/write FileStream.
        // This is required for incremental saving (Document.Save()).
        using (FileStream originalStream = new FileStream(originalPdfPath,
                                                          FileMode.Open,
                                                          FileAccess.ReadWrite))
        using (Document doc = new Document(originalStream))
        {
            // Append a new blank page to the document.
            doc.Pages.Add();

            // If an additional PDF is provided, append all its pages.
            if (File.Exists(pdfToAppendPath))
            {
                using (Document src = new Document(pdfToAppendPath))
                {
                    doc.Pages.Add(src.Pages);
                }
            }

            // Save incrementally. This writes the changes back to the same stream
            // without rewriting the entire file, preserving existing content.
            doc.Save();
        }

        // Copy the updated original file to the desired output location.
        // (The incremental update was performed in-place on originalPdfPath.)
        File.Copy(originalPdfPath, outputPdfPath, overwrite: true);
        Console.WriteLine($"Incremental update completed. Output saved to '{outputPdfPath}'.");
    }
}
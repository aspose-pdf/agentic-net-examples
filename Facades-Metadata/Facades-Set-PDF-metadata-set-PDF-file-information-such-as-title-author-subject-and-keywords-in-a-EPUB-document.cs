using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, a temporary PDF with updated metadata, and the final EPUB.
        const string inputPdfPath = "input.pdf";
        const string tempPdfPath = "temp_with_metadata.pdf";
        const string outputEpubPath = "output.epub";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // ---------- Set PDF metadata using the PdfFileInfo facade ----------
            var pdfInfo = new PdfFileInfo(inputPdfPath);
            pdfInfo.Title = "Sample EPUB Title";
            pdfInfo.Author = "John Doe";
            pdfInfo.Subject = "Sample Subject";
            pdfInfo.Keywords = "Aspose, PDF, EPUB";

            // Save the PDF with the new metadata to a temporary file.
            pdfInfo.Save(tempPdfPath);
            pdfInfo.Close(); // Release the facade resources.

            // ---------- Convert the updated PDF to EPUB ----------
            using (var document = new Document(tempPdfPath))
            {
                var epubOptions = new EpubSaveOptions
                {
                    // Transfer the PDF title to the EPUB title (optional).
                    Title = pdfInfo.Title
                };

                // Save the document as EPUB.
                document.Save(outputEpubPath, epubOptions);
            }

            // Clean up the temporary PDF file.
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);

            Console.WriteLine($"EPUB created successfully at '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
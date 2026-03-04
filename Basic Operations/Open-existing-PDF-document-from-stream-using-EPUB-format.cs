using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputEpubPath = "output.epub";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Open the PDF from a FileStream (stream‑based loading)
            using (FileStream pdfStream = File.OpenRead(inputPdfPath))
            using (Document pdfDoc = new Document(pdfStream))
            {
                // Configure EPUB save options
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Use flow layout for best EPUB compatibility
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow,
                    // Optional: set the EPUB title
                    Title = Path.GetFileNameWithoutExtension(outputEpubPath)
                };

                // Save the document as EPUB
                pdfDoc.Save(outputEpubPath, epubOptions);
            }

            Console.WriteLine($"EPUB file created at '{outputEpubPath}'.");
        }
        catch (PdfException ex)
        {
            // Handles errors specific to Aspose.Pdf processing
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
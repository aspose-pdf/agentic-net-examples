using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF file
        const string epubPath  = "output.epub"; // destination EPUB file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF from a file stream. The second argument (true) tells Aspose.Pdf
            // to close the stream when the Document is disposed, preventing resource leaks.
            using (FileStream pdfStream = File.OpenRead(pdfPath))
            using (Document pdfDoc = new Document(pdfStream, true))
            {
                // Configure EPUB save options.
                // Use the nested enum RecognitionMode via the EpubSaveOptions type.
                EpubSaveOptions epubOptions = new EpubSaveOptions
                {
                    // Choose the desired content recognition mode.
                    // Flow = full analysis, PdfFlow = preserve rendering order, Fixed = keep layout.
                    ContentRecognitionMode = EpubSaveOptions.RecognitionMode.Flow
                };

                // Save the PDF as EPUB.
                pdfDoc.Save(epubPath, epubOptions);
            }

            Console.WriteLine($"PDF successfully converted to EPUB: {epubPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputEpubPath  = "input.epub";
        const string outputEpubPath = "output.epub";

        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"File not found: {inputEpubPath}");
            return;
        }

        try
        {
            // Load the EPUB file as a PDF document using EpubLoadOptions
            using (Document pdfDoc = new Document(inputEpubPath, new EpubLoadOptions()))
            {
                // Use the PdfFileInfo facade to modify PDF metadata
                using (PdfFileInfo info = new PdfFileInfo(pdfDoc))
                {
                    // Set standard document properties
                    info.Title   = "New EPUB Title";
                    info.Author  = "John Doe";
                    info.Subject = "Sample EPUB converted to PDF and back";
                    info.Keywords = "Aspose.Pdf, EPUB, Metadata";

                    // Optionally set custom metadata
                    info.SetMetaInfo("CustomProperty", "CustomValue");
                }

                // Save the modified document back to EPUB format using EpubSaveOptions
                EpubSaveOptions epubSaveOptions = new EpubSaveOptions();
                pdfDoc.Save(outputEpubPath, epubSaveOptions);
            }

            Console.WriteLine($"EPUB saved with updated properties to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
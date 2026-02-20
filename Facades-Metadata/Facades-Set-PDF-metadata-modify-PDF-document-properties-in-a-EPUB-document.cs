using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // Facades namespace as required

class Program
{
    static void Main(string[] args)
    {
        // Input EPUB file path
        const string inputEpubPath = "input.epub";
        // Output EPUB file path
        const string outputEpubPath = "output.epub";

        // Verify the input file exists
        if (!File.Exists(inputEpubPath))
        {
            Console.Error.WriteLine($"Error: EPUB file not found at '{inputEpubPath}'.");
            return;
        }

        try
        {
            // Load the EPUB file into a Document object
            // EpubLoadOptions provides default page size (A4) and other settings
            Document pdfDoc = new Document(inputEpubPath, new EpubLoadOptions());

            // -----------------------------------------------------------------
            // Modify metadata using the core Document API (Info property)
            // -----------------------------------------------------------------
            pdfDoc.Info.Title = "Sample EPUB Title";
            pdfDoc.Info.Author = "John Doe";
            pdfDoc.Info.Subject = "Demonstration of metadata modification";
            pdfDoc.Info.Keywords = "Aspose.Pdf, EPUB, Metadata";

            // -----------------------------------------------------------------
            // Save the modified document back to EPUB format
            // -----------------------------------------------------------------
            // EpubSaveOptions allows setting EPUB‑specific properties (e.g., Title)
            EpubSaveOptions saveOptions = new EpubSaveOptions
            {
                Title = pdfDoc.Info.Title   // Ensure the EPUB title matches the PDF title
            };

            // Use the Document.Save method as per the provided save rule
            pdfDoc.Save(outputEpubPath, saveOptions);

            Console.WriteLine($"EPUB metadata updated and saved to '{outputEpubPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
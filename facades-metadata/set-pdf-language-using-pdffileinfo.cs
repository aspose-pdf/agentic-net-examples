using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the Facades PdfFileInfo class.
        // PdfFileInfo implements IDisposable, so wrap it in a using block.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Access the underlying Document object.
            Document doc = pdfInfo.Document;

            // Set the natural language for the entire document.
            // This updates the /Lang entry in the PDF catalog.
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // Save the updated PDF. SaveNewInfo writes only the modified
            // metadata (including the language) back to a new file.
            bool success = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(success
                ? $"Language set to 'en-US' and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}
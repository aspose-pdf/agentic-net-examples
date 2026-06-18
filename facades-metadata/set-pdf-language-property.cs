using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Access the underlying Document object
            Document doc = pdfInfo.Document;

            // Set the document language via the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // Save the updated PDF using the facade
            bool success = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(success
                ? $"Language set to 'en-US' and saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}
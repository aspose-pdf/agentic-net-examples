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
        // The constructor binds the file automatically.
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Access the underlying Document object.
            Document doc = pdfInfo.Document;

            // Set the natural language for the entire document via the tagged content API.
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // Save the updated PDF using PdfFileInfo.
            // SaveNewInfo writes the changes to a new file.
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Language set to 'en-US' and saved to '{outputPath}'."
                : $"Failed to save updated PDF to '{outputPath}'.");
        }
    }
}
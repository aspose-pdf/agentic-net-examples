using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // Facades namespace included as requested
using Aspose.Pdf.Tagged;           // ITaggedContent interface
using Aspose.Pdf.LogicalStructure; // Structure element types (not used directly here)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // PdfFileInfo does NOT expose a Language property.
            // The correct way to set the document language is via the TaggedContent API.
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");   // Set /Lang entry to "en-US"

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF language set to \"en-US\" and saved as '{outputPath}'.");
    }
}
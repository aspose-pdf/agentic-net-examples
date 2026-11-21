using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;          // PdfFileInfo resides here
using Aspose.Pdf.Tagged;          // ITaggedContent interface
using Aspose.Pdf.LogicalStructure; // StructureElement, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_language_set.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Use PdfFileInfo (from Aspose.Pdf.Facades) to read existing metadata.
        // PdfFileInfo does NOT provide a SetInfo method, so we only read here.
        // ------------------------------------------------------------
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        Console.WriteLine($"Title : {fileInfo.Title}");
        Console.WriteLine($"Author: {fileInfo.Author}");
        // Note: PdfFileInfo does not expose a Language property.

        // ------------------------------------------------------------
        // Load the PDF document, set the language via the tagged content API,
        // and save the changes.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a TaggedContent object (creates one if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Set the document language to "en-US"
            tagged.SetLanguage("en-US");

            // Optionally set a title for the tagged content
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language set to \"en-US\" and saved to '{outputPath}'.");
    }
}
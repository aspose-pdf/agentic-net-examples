using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newTitle   = "Accessible Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set the document title metadata (applies to the root structure element)
            taggedContent.SetTitle(newTitle);

            // Optionally, you can also set the language if needed
            // taggedContent.SetLanguage("en-US");

            // No need to call PreSave(); just save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved with title set to \"{newTitle}\" at '{outputPath}'.");
    }
}
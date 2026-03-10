using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create -> load -> use)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content interface
            ITaggedContent taggedContent = doc.TaggedContent;

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Read the language setting from the root element.
            // The Language property may be null if not set.
            string language = root.Language ?? string.Empty;

            Console.WriteLine($"Document language: {(string.IsNullOrEmpty(language) ? "(unknown)" : language)}");
        }
    }
}
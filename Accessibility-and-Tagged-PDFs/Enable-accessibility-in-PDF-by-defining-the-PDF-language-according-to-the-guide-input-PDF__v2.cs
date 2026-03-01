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
        const string outputPath = "output_with_language.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and set its language for accessibility
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a TaggedContent object if not present)
            ITaggedContent taggedContent = doc.TaggedContent;

            // Define the natural language for the entire document (e.g., English - United States)
            taggedContent.SetLanguage("en-US");

            // Optionally set a title for the tagged PDF
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with language set: {outputPath}");
    }
}
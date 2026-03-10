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
        const string title      = "Sample Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Set the document title in the standard metadata (DocumentInfo)
            doc.Info.Title = title;

            // Set the title in the tagged content (if the PDF is or will be tagged)
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetTitle(title);

            // Optionally, set a language for accessibility
            taggedContent.SetLanguage("en-US");

            // Save the updated PDF (lifecycle rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated title to '{outputPath}'.");
    }
}
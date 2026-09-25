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
        const string outputPath = "output_tagged.pdf";
        const string language   = "en-US";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Obtain the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set the primary language for the entire structure tree
            tagged.SetLanguage(language);

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' applied and saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF, set its primary language, and save.
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content and set the document language (e.g., English US).
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");

            // Persist the change.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language set and saved to '{outputPath}'.");
    }
}
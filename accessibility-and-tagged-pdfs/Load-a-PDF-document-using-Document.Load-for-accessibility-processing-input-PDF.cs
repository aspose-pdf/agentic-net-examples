using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

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

        // Load the PDF document. The Document constructor (or Load method) accepts a path
        // and internally uses the default LoadOptions, which is sufficient for most
        // accessibility scenarios.
        using (Document doc = new Document(inputPath))
        {
            // The document is now ready for accessibility processing.
            ITaggedContent taggedContent = doc.TaggedContent;

            // Example accessibility operations (commented out):
            // bool isTagged = doc.IsTagged; // read‑only property
            // taggedContent.SetLanguage("en-US");
        }

        Console.WriteLine("PDF document loaded successfully.");
    }
}

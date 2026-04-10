using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set the primary language for accessibility, and save
        using (Document doc = new Document(inputPath))
        {
            // In recent Aspose.PDF versions the TaggedContent property is read‑only and always returns a valid instance.
            // The older IsTagged property and direct assignment to TaggedContent are no longer available.
            ITaggedContent tagged = doc.TaggedContent;

            // Set the primary language (RFC 3066 tag) for screen‑readers and other assistive technologies.
            tagged.SetLanguage("en-US");

            // Optionally set a document title for accessibility tools.
            tagged.SetTitle("PDF with language set");

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with language set: {outputPath}");
    }
}

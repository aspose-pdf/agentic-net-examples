using System;
using System.IO;
using Aspose.Pdf;

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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content via dynamic to avoid compile‑time dependency on the TaggedContent type
            dynamic tagged = doc.TaggedContent;

            // Read language setting from the tagged PDF (if present)
            // The language information is stored in the LanguageInfo object.
            string language = tagged?.LanguageInfo?.Language ?? "(not set)";

            // Output language and some standard metadata
            Console.WriteLine($"Language (tagged PDF): {language}");
            Console.WriteLine($"Title (metadata): {doc.Info.Title}");
            Console.WriteLine($"Author (metadata): {doc.Info.Author}");
        }
    }
}

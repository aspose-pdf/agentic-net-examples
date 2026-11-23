using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has tagged content; enable auto‑tagging if it does not.
            if (doc.TaggedContent == null)
            {
                AutoTaggingSettings.Default.EnableAutoTagging = true;
                // After enabling auto‑tagging the same Document instance now exposes TaggedContent.
            }

            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Unable to obtain tagged content.");
                return;
            }

            // Root of the logical structure tree.
            var root = tagged.RootElement;

            // Create a new section element (custom tag "Sect").
            var section = tagged.CreateSectElement();

            // Create a level‑1 header element.
            var header = tagged.CreateHeaderElement(1);
            header.SetText("Custom Section Header");
            // Assign a custom PDF tag to the header element.
            header.SetTag("MyCustomHeader");

            // Build the hierarchy: section -> header, then attach section to root.
            section.AppendChild(header);
            root.AppendChild(section);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}

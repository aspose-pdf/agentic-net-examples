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
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Document does not support tagging.");
                return;
            }

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Retrieve all heading elements (H1‑H6) recursively
            var headings = root.FindElements<HeaderElement>(true);

            if (headings.Count == 0)
            {
                Console.WriteLine("No heading elements found.");
            }
            else
            {
                // Select the first heading element (or any specific one)
                HeaderElement heading = headings[0];

                // Assign a specific heading tag using the SetTag method.
                // Example: force this element to be an H2 heading.
                heading.SetTag("H2");

                // Other existing tags and attributes remain unchanged.
            }

            // Save the modified PDF; no PreSave call is required.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved tagged PDF to '{outputPath}'.");
    }
}

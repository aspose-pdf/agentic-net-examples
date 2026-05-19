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
        const string outputPath = "output_french.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // (Optional) set the document language if not already set
            tagged.SetLanguage("en-US");

            // Get the root of the structure tree
            StructureElement root = tagged.RootElement;

            // Locate the specific structure element to modify.
            // Example: take the first ParagraphElement in the tree.
            ParagraphElement target = null;
            var paragraphs = root.FindElements<ParagraphElement>(true);
            if (paragraphs.Count > 0)
                target = paragraphs[0] as ParagraphElement;

            if (target != null)
            {
                // Update the language property to French (RFC 3066 tag)
                target.Language = "fr-FR";
                Console.WriteLine("Structure element language set to French.");
            }
            else
            {
                Console.WriteLine("Target structure element not found.");
            }

            // Save the modified PDF (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
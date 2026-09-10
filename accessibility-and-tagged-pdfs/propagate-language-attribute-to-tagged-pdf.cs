using System;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for TextFragment
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string language = "en-US";

        // ------------------------------------------------------------
        // 1. Create a minimal PDF file so the example is self‑contained.
        // ------------------------------------------------------------
        using (Document seed = new Document())
        {
            // Add a page with some simple content.
            Page page = seed.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample text for tagging"));

            // Enable tagged PDF structure and set a document‑level language.
            ITaggedContent seedTagged = seed.TaggedContent;
            seedTagged.SetLanguage(language);

            // Save the placeholder file that will be re‑opened later.
            seed.Save(inputPath);
        }

        // ------------------------------------------------------------
        // 2. Load the PDF and propagate the language attribute to every
        //    structure element in the tagged content tree.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // (Optional) Ensure the document‑level language is set.
            taggedContent.SetLanguage(language);

            // Get the root structure element.
            StructureElement root = taggedContent.RootElement;

            // Retrieve all structure elements, including the root.
            var allElements = root.FindElements<StructureElement>(true);

            // Propagate the language attribute.
            foreach (StructureElement element in allElements)
            {
                element.Language = language;
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Language '{language}' propagated and saved to '{outputPath}'.");
    }
}

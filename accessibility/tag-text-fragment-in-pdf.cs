using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.LogicalStructure;   // Structure elements (SpanElement, etc.)
using Aspose.Pdf.Tagged;             // ITaggedContent

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the desired text fragment (e.g., the first occurrence of "Sample Text")
            TextFragmentAbsorber absorber = new TextFragmentAbsorber("Sample Text");
            doc.Pages.Accept(absorber);

            // Aspose.Pdf collections are 1‑based; get the first matching fragment
            if (absorber.TextFragments.Count > 0)
            {
                TextFragment fragment = absorber.TextFragments[1];

                // ----- Tagging the text using the Tagged PDF API -----
                // The TextFragment class does not expose a Tag property. To tag text we
                // create a structure element (SpanElement) via the ITaggedContent API and
                // add it to the logical structure tree.
                ITaggedContent tagged = doc.TaggedContent;          // Access the tagged‑content manager
                StructureElement root = tagged.RootElement;        // Root of the structure tree

                // Create a SpanElement, set its text to the fragment's text and append it
                // to the root (or any other appropriate parent element).
                SpanElement span = tagged.CreateSpanElement();
                span.SetText(fragment.Text);
                root.AppendChild(span);
                // -----------------------------------------------------

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("No matching text fragment found.");
            }
        }
    }
}

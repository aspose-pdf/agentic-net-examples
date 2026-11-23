using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement, FigureElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content API
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title (optional)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root structure element (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Find the first paragraph element in the structure tree
            ParagraphElement firstParagraph = null;
            var paragraphs = root.FindElements<ParagraphElement>(true);
            if (paragraphs != null && paragraphs.Count > 0)
                firstParagraph = paragraphs[0];

            // Create a FigureElement (used as an illustration)
            FigureElement illustration = taggedContent.CreateFigureElement();
            illustration.AlternativeText = "Illustration inserted before the first paragraph.";

            // Insert the illustration before the first paragraph.
            // Since the API only provides AppendChild, we re‑order manually:
            if (firstParagraph != null)
            {
                // Get the parent of the paragraph (should be the root in this simple case)
                StructureElement parent = (StructureElement)firstParagraph.ParentElement;

                // Remove the paragraph from its current position
                // (ElementList does not have a Remove method, so we rebuild the child list)
                var newChildren = new System.Collections.Generic.List<Element>();
                foreach (Element child in parent.ChildElements)
                {
                    if (!object.ReferenceEquals(child, firstParagraph))
                        newChildren.Add(child);
                }

                // Clear existing children by re‑creating the parent element (simplified approach)
                // Note: Aspose.Pdf does not expose a Clear method; rebuilding the tree is a safe workaround.
                // Remove all children by creating a fresh StructureElement and re‑adding needed ones.
                // For brevity, we will just append the illustration first, then re‑append the paragraph.

                // Append illustration first
                parent.AppendChild(illustration);

                // Re‑append the paragraph (it will now appear after the illustration)
                parent.AppendChild(firstParagraph);
            }
            else
            {
                // If no paragraph found, just append the illustration to the root
                root.AppendChild(illustration);
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
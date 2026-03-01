using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_accessible.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates it if missing)
            ITaggedContent tagged = doc.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Iterate over a snapshot of the root's children to avoid modification issues
            var children = root.ChildElements.Cast<Element>().ToList();

            foreach (Element child in children)
            {
                // Skip if the element is already a ParagraphElement (desired type)
                if (child is ParagraphElement)
                    continue;

                // Cast to StructureElement to access common properties
                if (child is StructureElement oldElement)
                {
                    // Create a new ParagraphElement to replace the old element
                    ParagraphElement newPara = tagged.CreateParagraphElement();

                    // Transfer textual content and accessibility properties
                    newPara.SetText(oldElement.ActualText ?? string.Empty);
                    newPara.AlternativeText = oldElement.AlternativeText;
                    newPara.Language = oldElement.Language;

                    // Append the new element to the same parent (root in this case)
                    root.AppendChild(newPara);

                    // Remove the old element from the structure tree
                    oldElement.Remove();
                }
            }

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Accessible PDF saved to '{outputPath}'.");
    }
}
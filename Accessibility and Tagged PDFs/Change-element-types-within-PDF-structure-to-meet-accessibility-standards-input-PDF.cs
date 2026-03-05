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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and obtain its tagged content interface
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent taggedContent = doc.TaggedContent;

            // Root of the logical structure tree (no cast needed)
            StructureElement root = taggedContent.RootElement;

            // Find all SpanElements in the document (deep search)
            var spanElements = root.FindElements<SpanElement>(true);

            foreach (SpanElement span in spanElements)
            {
                // Create a new ParagraphElement to replace the SpanElement
                ParagraphElement paragraph = taggedContent.CreateParagraphElement();

                // Preserve the textual content of the original span
                string text = span.ActualText ?? string.Empty;
                paragraph.SetText(text);

                // Determine the parent of the span (must be a StructureElement)
                StructureElement parent = span.ParentElement as StructureElement;
                if (parent != null)
                {
                    // Locate the span's position within its parent's child collection
                    int spanIndex = -1;
                    for (int i = 0; i < parent.ChildElements.Count; i++)
                    {
                        if (parent.ChildElements[i] == span)
                        {
                            spanIndex = i;
                            break;
                        }
                    }

                    if (spanIndex != -1)
                    {
                        // Insert the new paragraph at the same position as the span
                        // The boolean parameter defaults to true; passing false is safe here
                        parent.InsertChild(paragraph, spanIndex, false);

                        // Remove the original span from the structure
                        span.Remove();
                    }
                }
            }

            // Save the modified PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with updated structure saved to '{outputPath}'.");
    }
}
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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Verify that the PDF has tagged content. If it does not, enable auto‑tagging.
            if (doc.TaggedContent == null)
            {
                AutoTaggingSettings.Default.EnableAutoTagging = true;
                // Depending on the Aspose.Pdf version you may need to re‑run auto‑tagging here.
                // For the purpose of this example we continue; newly created elements will be added to the logical structure.
            }

            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a TOCI (Table of Contents Item) element and attach it to the root
            var toci = tagged.CreateTOCIElement();
            root.AppendChild(toci);

            // Create a paragraph element that will be placed under the TOCI element
            ParagraphElement para = tagged.CreateParagraphElement();
            para.SetText("This is a paragraph under a TOCI element.");
            // Set the ActualText property for accessibility (used by screen readers)
            para.ActualText = "Accessible description of the paragraph.";

            // Append the paragraph to the TOCI element
            toci.AppendChild(para);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}

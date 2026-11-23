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
            ITaggedContent taggedContent = doc.TaggedContent;

            // Set language and title for the tagged PDF
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle("Nested Structure Example");

            // Root element of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Level 1 heading
            HeaderElement h1 = taggedContent.CreateHeaderElement(1);
            h1.SetText("Chapter 1");
            root.AppendChild(h1); // attach to root

            // Paragraph under Level 1 heading
            ParagraphElement p1 = taggedContent.CreateParagraphElement();
            p1.SetText("This is the first paragraph under Chapter 1.");
            h1.AppendChild(p1); // attach to heading

            // Level 2 heading (child of Level 1 heading)
            HeaderElement h2 = taggedContent.CreateHeaderElement(2);
            h2.SetText("Section 1.1");
            h1.AppendChild(h2);

            // Paragraph under Level 2 heading
            ParagraphElement p2 = taggedContent.CreateParagraphElement();
            p2.SetText("Paragraph under Section 1.1.");
            h2.AppendChild(p2);

            // Level 3 heading (child of Level 2 heading)
            HeaderElement h3 = taggedContent.CreateHeaderElement(3);
            h3.SetText("Subsection 1.1.1");
            h2.AppendChild(h3);

            // Paragraph under Level 3 heading
            ParagraphElement p3 = taggedContent.CreateParagraphElement();
            p3.SetText("Paragraph under Subsection 1.1.1.");
            h3.AppendChild(p3);

            // Save the modified PDF (no PreSave required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
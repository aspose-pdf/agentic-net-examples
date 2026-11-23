using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF to tag (can be empty)
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF and work with its tagged content
        using (Document doc = new Document(inputPath))
        {
            // Obtain the ITaggedContent interface
            ITaggedContent tagged = doc.TaggedContent;

            // Set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree (no cast required)
            StructureElement root = tagged.RootElement;

            // First level heading (H1)
            HeaderElement h1 = tagged.CreateHeaderElement(1);
            h1.SetText("Chapter 1");
            root.AppendChild(h1);

            // Paragraph under H1
            ParagraphElement p1 = tagged.CreateParagraphElement();
            p1.SetText("This is the first paragraph under Chapter 1.");
            h1.AppendChild(p1);

            // Second level heading (H2) under the root
            HeaderElement h2 = tagged.CreateHeaderElement(2);
            h2.SetText("Section 1.1");
            root.AppendChild(h2);

            // First paragraph under H2
            ParagraphElement p2 = tagged.CreateParagraphElement();
            p2.SetText("This is the first paragraph under Section 1.1.");
            h2.AppendChild(p2);

            // Second paragraph under H2
            ParagraphElement p3 = tagged.CreateParagraphElement();
            p3.SetText("This is the second paragraph under Section 1.1.");
            h2.AppendChild(p3);

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
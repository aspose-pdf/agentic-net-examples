using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // for WebHyperlink

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged content interface
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set language and title for the tagged PDF
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ----- Create a new section (SectElement) -----
            SectElement section = tagged.CreateSectElement();

            // Add a heading for the section
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Resources");
            section.AppendChild(heading);

            // ----- Create a paragraph with a link inside -----
            ParagraphElement paragraph = tagged.CreateParagraphElement();
            paragraph.SetText("For more information, visit ");

            // Create the link element
            LinkElement link = tagged.CreateLinkElement();
            link.SetText("Aspose website");
            // Use WebHyperlink to define the target URL (no Uri property exists)
            link.Hyperlink = new WebHyperlink("https://www.aspose.com");

            // Append the link to the paragraph
            paragraph.AppendChild(link);

            // Append the paragraph to the section
            section.AppendChild(paragraph);

            // Attach the new section to the root of the structure tree
            root.AppendChild(section);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}

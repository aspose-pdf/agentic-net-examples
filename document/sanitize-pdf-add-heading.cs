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
        const string outputPath = "clean_navigable.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging for the new document (optional but helpful)
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Load the original PDF
        using (Document src = new Document(inputPath))
        {
            // Create a fresh PDF that will contain only the sanitized content
            using (Document dest = new Document())
            {
                // Copy all pages from the source to the destination
                foreach (Page page in src.Pages)
                {
                    dest.Pages.Add(page);
                }

                // Obtain the tagging interface
                ITaggedContent tagged = dest.TaggedContent;

                // Set language and title for accessibility
                tagged.SetLanguage("en-US");
                tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root of the logical structure tree (no cast needed)
                StructureElement root = tagged.RootElement;

                // Create a top‑level heading (H1) for the document title
                HeaderElement docTitle = tagged.CreateHeaderElement(1);
                docTitle.SetText("Document Title");
                root.AppendChild(docTitle);

                // Create a section heading (H2)
                HeaderElement section = tagged.CreateHeaderElement(2);
                section.SetText("Section 1");
                root.AppendChild(section);

                // Add a paragraph under the section
                ParagraphElement paragraph = tagged.CreateParagraphElement();
                paragraph.SetText("This is the first paragraph of the section.");
                root.AppendChild(paragraph);

                // Save the cleaned, navigable PDF
                dest.Save(outputPath);
            }
        }

        Console.WriteLine($"Clean PDF saved to '{outputPath}'.");
    }
}
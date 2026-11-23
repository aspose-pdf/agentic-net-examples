using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;               // ITaggedContent
using Aspose.Pdf.LogicalStructure;    // StructureElement, ParagraphElement

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_with_footer.pdf";

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

            // Set language and title (optional, but good practice for accessibility)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a new paragraph element that will serve as the footer
            ParagraphElement footer = taggedContent.CreateParagraphElement();
            footer.SetText("© 2026 My Company – All rights reserved.");
            // Optionally set language on the element itself
            footer.Language = "en-US";

            // Append the footer as the last child of the root element
            root.AppendChild(footer);

            // Save the modified PDF (still a PDF, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF with footer saved to '{outputPath}'.");
    }
}
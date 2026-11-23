using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Annotations; // for WebHyperlink

class Program
{
    static void Main()
    {
        const string inputPath = "input_tagged.pdf";
        const string outputPath = "output_with_link.pdf";

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

            // Optionally set language and title (write‑only setters)
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Get the root of the structure tree (no cast required)
            StructureElement root = taggedContent.RootElement;

            // Create a link element via the ITaggedContent factory
            LinkElement link = taggedContent.CreateLinkElement();

            // Set the visible text of the link
            link.SetText("Visit Aspose");

            // Set the target URI for the link using the correct property
            link.Hyperlink = new WebHyperlink("https://www.aspose.com");

            // Attach the link element to the root of the structure tree
            root.AppendChild(link); // default checkIfCanBeAppended = true

            // Save the modified PDF (no PreSave call needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with link element to '{outputPath}'.");
    }
}

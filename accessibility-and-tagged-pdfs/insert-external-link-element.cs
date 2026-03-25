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
        const string url = "https://example.com";
        const string linkTitle = "Example Site";
        const string linkText = "Visit Example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates one if missing)
                ITaggedContent tagged = doc.TaggedContent;
                // Ensure the document has a root element
                StructureElement root = tagged.RootElement;

                // Create a Link element
                LinkElement link = tagged.CreateLinkElement();
                link.SetText(linkText);               // visible text for the link
                link.Title = linkTitle;               // title attribute
                link.Hyperlink = new WebHyperlink(url); // external URL

                // Attach the link to the document structure
                root.AppendChild(link);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Link element added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
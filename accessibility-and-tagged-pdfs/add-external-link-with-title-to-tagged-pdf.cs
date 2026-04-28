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
        const string url        = "https://www.example.com";
        const string title      = "Example Site";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a LinkElement via the ITaggedContent factory
            LinkElement link = tagged.CreateLinkElement();

            // Optional: set the visible text for the link element
            link.SetText("Visit Example");

            // Assign an external web hyperlink
            link.Hyperlink = new WebHyperlink(url);

            // Define the title attribute
            link.Title = title;

            // Attach the link element to the document's structure tree
            root.AppendChild(link);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Link element added and saved to '{outputPath}'.");
    }
}
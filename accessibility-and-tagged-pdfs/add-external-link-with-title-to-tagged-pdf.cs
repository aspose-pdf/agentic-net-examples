using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_link.pdf";
        const string url = "https://www.example.com";
        const string title = "Example Site";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Optional: ensure the document has tagged content
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // -------------------------------------------------
            // Add a visual link annotation on the first page
            // -------------------------------------------------
            Page page = doc.Pages[1];

            // Define the clickable rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the link annotation and set its URI action
            LinkAnnotation link = new LinkAnnotation(page, rect);
            link.Action = new GoToURIAction(url);

            // Use the Contents property as the title/tooltip for the link
            link.Contents = title;

            // Underline the link to make it recognizable – set border via Border object
            link.Border = new Border(link)
            {
                Style = BorderStyle.Underline,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(link);

            // -------------------------------------------------
            // Add a logical structure link element (tagged PDF)
            // -------------------------------------------------
            StructureElement root = tagged.RootElement;

            // Create a LinkElement via the ITaggedContent factory
            LinkElement linkElem = tagged.CreateLinkElement();

            // Set the visible text of the link element
            linkElem.SetText(title);

            // Set the AlternativeText property as the title attribute
            linkElem.AlternativeText = title;

            // Attach the link element to the structure tree
            root.AppendChild(linkElem);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with external link saved to '{outputPath}'.");
    }
}

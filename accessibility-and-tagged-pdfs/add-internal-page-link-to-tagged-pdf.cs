using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.Annotations; // for GoToAction
using Aspose.Pdf.Text; // required for TextFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPage = 2; // internal page number to link to (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            // (optional) set document title / language for accessibility
            tagged.SetTitle("Document with internal link");
            tagged.SetLanguage("en-US");

            // Root of the structure tree – use dynamic to avoid compile‑time dependency on StructureElement
            dynamic root = tagged.RootElement;

            // ------------------------------------------------------------
            // 1. Create a structure element whose PDF role is "/Link"
            // ------------------------------------------------------------
            // The tagged API provides a method to create a Link element.
            // Using dynamic bypasses the need for the concrete StructureElement type.
            dynamic linkElement = root.CreateLinkElement();

            // ------------------------------------------------------------
            // 2. Define an internal destination (GoTo action) pointing to the target page.
            // ------------------------------------------------------------
            linkElement.Action = new GoToAction(doc.Pages[targetPage]); // Pages collection is 1‑based

            // ------------------------------------------------------------
            // 3. Add visible text that will act as the clickable link.
            // ------------------------------------------------------------
            TextFragment linkText = new TextFragment($"Go to page {targetPage}");
            linkText.TextState.FontSize = 12;
            // Add the text fragment to the first page (or any page you prefer)
            doc.Pages[1].Paragraphs.Add(linkText);

            // Associate the text fragment with the link element so that the
            // structure tree knows the text belongs to the /Link element.
            linkElement.AppendChild(linkText);

            // Finally, attach the link element to the document's structure tree.
            root.AppendChild(linkElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with internal link saved to '{outputPath}'.");
    }
}
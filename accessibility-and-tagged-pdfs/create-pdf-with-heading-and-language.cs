using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "heading.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a visual page so the PDF is not empty
            Page page = doc.Pages.Add();

            // Create a text fragment that will be displayed on the page
            TextFragment tf = new TextFragment("Chapter 1: Introduction");
            page.Paragraphs.Add(tf);

            // Access the tagging API
            ITaggedContent tagged = doc.TaggedContent;

            // Optional: set document-level language and title
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Document with Heading");

            // Get the root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a heading element (level 1) and set its displayed text
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText("Chapter 1: Introduction");

            // Assign a language attribute to the heading element
            heading.Language = "en-US";

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
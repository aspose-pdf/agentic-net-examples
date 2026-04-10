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
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable auto‑tagging so that headings are detected and structure is built
            AutoTaggingSettings.Default.EnableAutoTagging = true;
            // (Optional) configure heading levels if custom mapping is required
            // AutoTaggingSettings.Default.HeadingLevels = new HeadingLevels();

            // Process the document to create logical structure based on headings
            doc.ProcessParagraphs();

            // Access the tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            // Set language and title for the tagged PDF (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Create a new page that will hold the Table of Contents
            Page tocPage = doc.Pages.Insert(1);
            // Configure TOC page info (show page numbers, set title, etc.)
            tocPage.TocInfo = new TocInfo
            {
                IsShowPageNumbers = true,
                Title = new TextFragment("Table of Contents") // Title expects a TextFragment
            };

            // Create the TOC element and add it to the structure tree
            TOCElement tocElement = tagged.CreateTOCElement();
            tocElement.Title = "Table of Contents"; // Visible title of the TOC
            // The TOC element is automatically associated with the page inserted above;
            // no need (and not allowed) to set the Page property directly.

            // Append the TOC element to the root of the structure tree
            StructureElement root = tagged.RootElement;
            root.AppendChild(tocElement);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑level TOC saved to '{outputPath}'.");
    }
}

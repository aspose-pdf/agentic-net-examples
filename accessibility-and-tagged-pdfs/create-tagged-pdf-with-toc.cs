using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_toc.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API and set basic metadata
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // ------------------------------------------------------------
            // 1. Create a new page that will hold the visual Table of Contents
            // ------------------------------------------------------------
            Page tocPage = doc.Pages.Add();

            // Add a visible title on the TOC page
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.FontSize = 20;
            tocTitle.TextState.FontStyle = FontStyles.Bold;
            tocTitle.Position = new Position(50, 800);
            tocPage.Paragraphs.Add(tocTitle);

            // ------------------------------------------------------------
            // 2. Build the logical structure for the TOC page
            // ------------------------------------------------------------
            // Root element of the document's logical structure
            StructureElement root = tagged.RootElement;

            // Create a heading element (H1) for the TOC page itself
            HeaderElement tocHeader = tagged.CreateHeaderElement(1);
            tocHeader.SetText("Table of Contents");
            root.AppendChild(tocHeader); // AppendChild with one argument (default bool)

            // ------------------------------------------------------------
            // 3. Add heading entries (H2) to the logical structure
            // ------------------------------------------------------------
            // Example headings – in a real scenario these would be extracted from the document
            string[] headings = {
                "Chapter 1 – Introduction",
                "Chapter 2 – Methods",
                "Chapter 3 – Results"
            };

            int pageNumber = 2; // Assuming content starts on page 2 after the TOC

            foreach (string heading in headings)
            {
                // Create a heading element (H2) for each entry
                HeaderElement h2 = tagged.CreateHeaderElement(2);
                h2.SetText(heading);
                root.AppendChild(h2);

                // Add a visible line on the TOC page showing the heading and its page number
                TextFragment entry = new TextFragment($"{heading} .......... {pageNumber}");
                entry.Position = new Position(50, 750 - (pageNumber - 2) * 20);
                tocPage.Paragraphs.Add(entry);

                pageNumber++;
            }

            // ------------------------------------------------------------
            // 4. Save the modified PDF (no PreSave required)
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document with tagged Table of Contents saved to '{outputPath}'.");
    }
}
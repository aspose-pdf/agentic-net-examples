using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertFootnotes
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string footnotesXmlPath = "footnotes.xml";
        const string outputPdfPath  = "output_with_footnotes.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footnotesXmlPath))
        {
            Console.Error.WriteLine($"XML not found: {footnotesXmlPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load footnote definitions from XML
            XDocument xmlDoc = XDocument.Load(footnotesXmlPath);
            // Expected XML format:
            // <footnotes>
            //   <footnote id="1">First footnote text.</footnote>
            //   <footnote id="2">Second footnote text.</footnote>
            // </footnotes>

            // Simple example: add each footnote as a separate paragraph on the first page
            Page firstPage = pdfDoc.Pages[1]; // page indexing is 1‑based (rule: page-indexing-one-based)

            foreach (XElement fnElement in xmlDoc.Root.Elements("footnote"))
            {
                string footnoteText = fnElement.Value.Trim();

                // Create a TextFragment for the main content (could be empty if only footnote is needed)
                TextFragment mainFragment = new TextFragment(string.Empty);
                // Assign the footnote to the TextFragment (property FootNote expects a Note object)
                mainFragment.FootNote = new Note(footnoteText);

                // Position the fragment near the bottom of the page (adjust as needed)
                // Using Aspose.Pdf.Text.Position; avoid System.Drawing types.
                mainFragment.Position = new Position(50, 50); // 50 points from left, 50 points from bottom

                // Add the fragment to the page's paragraph collection
                firstPage.Paragraphs.Add(mainFragment);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPdfPath}'.");
    }
}
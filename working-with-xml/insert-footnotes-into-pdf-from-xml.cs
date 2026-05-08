using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class InsertFootnotes
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string footnotesXmlPath = "footnotes.xml";
        const string outputPdfPath = "output_with_footnotes.pdf";

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

        // Load footnote definitions from XML.
        // Expected format:
        // <footnotes>
        //   <footnote id="1">First footnote text.</footnote>
        //   <footnote id="2">Second footnote text.</footnote>
        // </footnotes>
        var footnoteMap = new Dictionary<string, string>();
        try
        {
            XDocument xmlDoc = XDocument.Load(footnotesXmlPath);
            foreach (var fn in xmlDoc.Descendants("footnote"))
            {
                var idAttr = fn.Attribute("id");
                if (idAttr != null)
                {
                    footnoteMap[idAttr.Value] = fn.Value.Trim();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        // Open the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Optional: ensure the document is tagged (required for proper accessibility handling).
            ITaggedContent tagged = pdfDoc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPdfPath));

            // For each page, insert footnotes at the bottom.
            // This example adds all footnotes to every page; adapt as needed.
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Starting Y coordinate for footnotes (bottom margin = 50 points).
                double footnoteY = 50;
                foreach (var kvp in footnoteMap)
                {
                    // Create a TextFragment that will hold the footnote reference.
                    // The visible text can be the footnote number in brackets.
                    string footnoteNumber = kvp.Key;
                    TextFragment tf = new TextFragment($"[{footnoteNumber}]");
                    tf.Position = new Position(50, footnoteY); // left margin = 50
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.FontSize = 10;
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Attach the actual footnote content using the FootNote property.
                    // The Note object holds the explanatory text.
                    tf.FootNote = new Note(kvp.Value);

                    // Add the fragment to the page.
                    page.Paragraphs.Add(tf);

                    // Move up for the next footnote.
                    footnoteY += 12; // line spacing
                }
            }

            // Save the modified PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPdfPath}'.");
    }
}
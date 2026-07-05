using System;
using System.IO;
using System.Linq;
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
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(footnotesXmlPath))
        {
            Console.Error.WriteLine($"Footnotes XML not found: {footnotesXmlPath}");
            return;
        }

        // Load footnote definitions from XML.
        // Expected format:
        // <footnotes>
        //   <footnote page="1">First footnote text.</footnote>
        //   <footnote page="2">Second footnote text.</footnote>
        //   ...
        // </footnotes>
        XDocument xmlDoc = XDocument.Load(footnotesXmlPath);
        var footnoteData = xmlDoc.Root
                                 .Elements("footnote")
                                 .Select(fn => new
                                 {
                                     PageNumber = (int)fn.Attribute("page"),
                                     Text       = (string)fn.Value
                                 })
                                 .ToList();

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over each page in the PDF (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                // Find footnotes that belong to the current page.
                var notesForPage = footnoteData
                                   .Where(fn => fn.PageNumber == pageIndex)
                                   .Select(fn => fn.Text)
                                   .ToList();

                if (!notesForPage.Any())
                    continue; // No footnotes for this page.

                Page page = pdfDoc.Pages[pageIndex];
                // Use TextBuilder to add footnote fragments to the page.
                TextBuilder builder = new TextBuilder(page);

                // Position footnotes near the bottom of the page.
                // Adjust Y coordinate as needed (e.g., 50 points from bottom).
                double footnoteY = 50;
                double footnoteX = 50;
                double lineHeight = 12; // Approximate line height for spacing.

                foreach (string noteText in notesForPage)
                {
                    // Create a TextFragment that will hold the visible footnote reference.
                    TextFragment tf = new TextFragment(noteText);
                    tf.Position = new Position(footnoteX, footnoteY);
                    tf.TextState.FontSize = 9;
                    tf.TextState.Font = FontRepository.FindFont("Helvetica");
                    tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Create a Note object (the actual footnote content) and assign it.
                    Note note = new Note();
                    note.Text = noteText; // The explanatory text for the footnote.
                    tf.FootNote = note;   // Link the Note to the TextFragment.

                    // Append the fragment to the page.
                    builder.AppendText(tf);

                    // Move up for the next footnote.
                    footnoteY += lineHeight;
                }
            }

            // Save the modified PDF using the standard disposal pattern.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Footnotes inserted and saved to '{outputPdfPath}'.");
    }
}
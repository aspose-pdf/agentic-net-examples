using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithFootnotes.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a table and set its position
            Table table = new Table
            {
                ColumnWidths = "100 150 100", // three columns
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // ----- Row 1 -----
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Item");
            row1.Cells.Add("Description");
            row1.Cells.Add("Price");

            // ----- Row 2 (with footnote reference) -----
            Row row2 = table.Rows.Add();

            // Cell 1
            row2.Cells.Add("Widget A");

            // Cell 2 – contains a footnote reference (superscript ¹)
            // Use Unicode superscript 1 (U+00B9)
            string footnoteRef = "High‑quality widget\u00B9";
            row2.Cells.Add(footnoteRef);

            // Cell 3 – price
            row2.Cells.Add("$25.00");

            // ----- Row 3 (with another footnote) -----
            Row row3 = table.Rows.Add();
            row3.Cells.Add("Gadget B");
            // Superscript ² (U+00B2)
            string footnoteRef2 = "Advanced gadget\u00B2";
            row3.Cells.Add(footnoteRef2);
            row3.Cells.Add("$40.00");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Add some space after the table
            page.Paragraphs.Add(new TextFragment("\n"));

            // ----- Footnote section at the end of the document -----
            // We'll place footnotes on a new page for clarity
            Page footnotePage = doc.Pages.Add();

            // Footnote 1
            TextFragment footnote1 = new TextFragment("\u00B9 This widget meets ISO 9001 standards.");
            footnote1.TextState.FontSize = 10;
            footnotePage.Paragraphs.Add(footnote1);

            // Footnote 2
            TextFragment footnote2 = new TextFragment("\u00B2 The gadget includes a patented cooling system.");
            footnote2.TextState.FontSize = 10;
            footnotePage.Paragraphs.Add(footnote2);

            // ----- Create link annotations for the footnote references -----
            // Approximate rectangle positions for the superscript numbers.
            // In a real scenario you would calculate the exact coordinates.
            // Here we place a small rectangle near the top‑right corner of the cell.

            // First footnote link (row 2, column 2)
            // Approximate coordinates (values may need adjustment)
            Aspose.Pdf.Rectangle linkRect1 = new Aspose.Pdf.Rectangle(250, 650, 260, 660);
            LinkAnnotation link1 = new LinkAnnotation(page, linkRect1);
            // Destination: the footnote page (fit to window)
            link1.Destination = new FitExplicitDestination(footnotePage);
            page.Annotations.Add(link1);

            // Second footnote link (row 3, column 2)
            Aspose.Pdf.Rectangle linkRect2 = new Aspose.Pdf.Rectangle(250, 620, 260, 630);
            LinkAnnotation link2 = new LinkAnnotation(page, linkRect2);
            link2.Destination = new FitExplicitDestination(footnotePage);
            page.Annotations.Add(link2);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created: {outputPath}");
    }
}
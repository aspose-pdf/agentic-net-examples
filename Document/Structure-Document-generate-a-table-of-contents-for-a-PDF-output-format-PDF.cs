using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_toc.pdf";

        try
        {
            // Create a new PDF document
            Document pdfDoc = new Document();

            // Keep track of headings and the pages they appear on
            List<(string Title, Page Page)> headings = new List<(string, Page)>();

            // -----------------------------------------------------------------
            // Add content pages with headings (e.g., three chapters)
            // -----------------------------------------------------------------
            for (int i = 1; i <= 3; i++)
            {
                // Add a new page (pages are 1‑based)
                Page page = pdfDoc.Pages.Add();

                // Heading text
                string headingText = $"Chapter {i}";
                TextFragment heading = new TextFragment(headingText);
                heading.TextState.FontSize = 24;
                heading.TextState.FontStyle = FontStyles.Bold;
                heading.Position = new Position(0, 800); // near top of page
                page.Paragraphs.Add(heading);

                // Sample paragraph
                TextFragment paragraph = new TextFragment(
                    $"This is the content of {headingText}. Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
                paragraph.Position = new Position(0, 750);
                page.Paragraphs.Add(paragraph);

                // Store heading information
                headings.Add((headingText, page));
            }

            // -----------------------------------------------------------------
            // Insert a Table of Contents page at the beginning
            // -----------------------------------------------------------------
            Page tocPage = pdfDoc.Pages.Insert(1);

            // TOC title
            TextFragment tocTitle = new TextFragment("Table of Contents");
            tocTitle.TextState.FontSize = 28;
            tocTitle.TextState.FontStyle = FontStyles.Bold;
            tocTitle.Position = new Position(0, 800);
            tocPage.Paragraphs.Add(tocTitle);

            // Create a table for TOC entries
            Table tocTable = new Table
            {
                ColumnWidths = "400 100", // Title column, Page number column
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row headerRow = tocTable.Rows.Add();
            Cell hdrTitle = headerRow.Cells.Add();
            hdrTitle.Paragraphs.Add(new TextFragment("Title"));
            Cell hdrPage = headerRow.Cells.Add();
            hdrPage.Paragraphs.Add(new TextFragment("Page"));

            // Add a row for each heading
            foreach (var (title, targetPage) in headings)
            {
                Row row = tocTable.Rows.Add();

                // Title cell
                Cell titleCell = row.Cells.Add();
                titleCell.Paragraphs.Add(new TextFragment(title));

                // Page number cell (account for the inserted TOC page)
                int pageNumber = targetPage.Number + 1; // pages shifted by the TOC page
                Cell pageCell = row.Cells.Add();
                pageCell.Paragraphs.Add(new TextFragment(pageNumber.ToString()));

                // Create a link annotation on the TOC page that jumps to the target page
                // The rectangle is a zero‑size placeholder; Aspose.Pdf will still honor the action.
                LinkAnnotation link = new LinkAnnotation(tocPage, new Aspose.Pdf.Rectangle(0, 0, 0, 0))
                {
                    Action = new GoToAction(targetPage)
                };
                tocPage.Annotations.Add(link);
            }

            // Add the TOC table to the TOC page
            tocPage.Paragraphs.Add(tocTable);

            // -----------------------------------------------------------------
            // Save the document
            // -----------------------------------------------------------------
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
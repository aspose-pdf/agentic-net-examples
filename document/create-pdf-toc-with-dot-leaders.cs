using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CreatePdfWithToc
{
    static void Main()
    {
        // Output file path
        const string outputPath = "toc.pdf";

        // Sample TOC entries (title and target page number)
        var tocEntries = new (string Title, int Page)[]
        {
            ("Introduction", 1),
            ("Getting Started", 5),
            ("Advanced Topics", 12),
            ("Conclusion", 20)
        };

        // Determine the longest title length for dot leader calculation
        int maxTitleLength = 0;
        foreach (var entry in tocEntries)
        {
            if (entry.Title.Length > maxTitleLength)
                maxTitleLength = entry.Title.Length;
        }

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page for the Table of Contents
            Aspose.Pdf.Page tocPage = doc.Pages.Add();

            // Create a table with two columns: Title (with dot leaders) and Page number
            Aspose.Pdf.Table tocTable = new Aspose.Pdf.Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "70% 30%"
            };

            // Header row
            Aspose.Pdf.Row headerRow = tocTable.Rows.Add();
            Aspose.Pdf.Cell headerTitleCell = headerRow.Cells.Add();
            Aspose.Pdf.Cell headerPageCell = headerRow.Cells.Add();

            Aspose.Pdf.Text.TextFragment headerTitle = new Aspose.Pdf.Text.TextFragment("Section");
            // Configure header title style via the existing TextState instance
            headerTitle.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold");
            headerTitle.TextState.FontSize = 14;
            headerTitle.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            headerTitleCell.Paragraphs.Add(headerTitle);

            Aspose.Pdf.Text.TextFragment headerPage = new Aspose.Pdf.Text.TextFragment("Page");
            headerPage.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold");
            headerPage.TextState.FontSize = 14;
            headerPage.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            headerPageCell.Paragraphs.Add(headerPage);

            // Add each TOC entry as a row
            foreach (var entry in tocEntries)
            {
                Aspose.Pdf.Row row = tocTable.Rows.Add();

                // Title cell with dot leaders
                Aspose.Pdf.Cell titleCell = row.Cells.Add();
                // Calculate number of dots needed to fill space (simple approach)
                int dotCount = Math.Max(5, 40 - entry.Title.Length);
                string dots = new string('.', dotCount);
                string titleLine = $"{entry.Title}{dots}{entry.Page}";
                Aspose.Pdf.Text.TextFragment titleFragment = new Aspose.Pdf.Text.TextFragment(titleLine);
                // Configure title style via the existing TextState instance
                titleFragment.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
                titleFragment.TextState.FontSize = 12;
                titleFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                titleCell.Paragraphs.Add(titleFragment);

                // Page number cell (optional, kept empty because page number already in line)
                Aspose.Pdf.Cell pageCell = row.Cells.Add();
                // Leave empty or could add page number separately
            }

            // Add the table to the page
            tocPage.Paragraphs.Add(tocTable);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Table of Contents saved to '{outputPath}'.");
    }
}

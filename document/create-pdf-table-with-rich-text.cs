using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "RichTextTable.pdf";

        // Ensure any previous file is removed – prevents file‑lock issues during repeated builds
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }

        // Create a new PDF document inside a using block (ensures disposal)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table
            {
                // Position the table (coordinates are in points)
                Left = 50,
                Top = 700,
                // Optional: set table border
                Border = new BorderInfo(BorderSide.All, 1, Color.Black)
            };

            // -------------------------
            // Row 1
            // -------------------------
            // Create first row
            Row row1 = table.Rows.Add();

            // Cell 1, Row 1 – bold red text
            Cell cell11 = row1.Cells.Add();
            TextFragment tf11 = new TextFragment("Bold Red");
            tf11.TextState.Font = FontRepository.FindFont("Helvetica");
            tf11.TextState.FontSize = 12;
            tf11.TextState.FontStyle = FontStyles.Bold;
            tf11.TextState.ForegroundColor = Color.Red;
            cell11.Paragraphs.Add(tf11);

            // Cell 2, Row 1 – italic blue text
            Cell cell12 = row1.Cells.Add();
            TextFragment tf12 = new TextFragment("Italic Blue");
            tf12.TextState.Font = FontRepository.FindFont("Helvetica");
            tf12.TextState.FontSize = 12;
            tf12.TextState.FontStyle = FontStyles.Italic;
            tf12.TextState.ForegroundColor = Color.Blue;
            cell12.Paragraphs.Add(tf12);

            // -------------------------
            // Row 2
            // -------------------------
            Row row2 = table.Rows.Add();

            // Cell 1, Row 2 – underlined green text
            Cell cell21 = row2.Cells.Add();
            TextFragment tf21 = new TextFragment("Underline Green");
            tf21.TextState.Font = FontRepository.FindFont("Helvetica");
            tf21.TextState.FontSize = 12;
            tf21.TextState.Underline = true; // Underline is a bool property, not a FontStyle
            tf21.TextState.ForegroundColor = Color.Green;
            cell21.Paragraphs.Add(tf21);

            // Cell 2, Row 2 – regular black text with larger size
            Cell cell22 = row2.Cells.Add();
            TextFragment tf22 = new TextFragment("Large Black");
            tf22.TextState.Font = FontRepository.FindFont("Helvetica");
            tf22.TextState.FontSize = 16;
            tf22.TextState.ForegroundColor = Color.Black;
            cell22.Paragraphs.Add(tf22);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document as PDF (overwrites if the file already exists)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }
}

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

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position on the page
            Table table = new Table();

            // -------------------------
            // Row 1
            // -------------------------
            // Create first row
            Row row1 = table.Rows.Add();

            // Cell 1 – bold red text
            Cell cell11 = row1.Cells.Add();
            TextFragment tf11 = new TextFragment("Bold Red");
            tf11.TextState.Font = FontRepository.FindFont("Helvetica");
            tf11.TextState.FontSize = 12;
            tf11.TextState.ForegroundColor = Color.Red;
            tf11.TextState.FontStyle = FontStyles.Bold;
            cell11.Paragraphs.Add(tf11);

            // Cell 2 – italic blue text
            Cell cell12 = row1.Cells.Add();
            TextFragment tf12 = new TextFragment("Italic Blue");
            tf12.TextState.Font = FontRepository.FindFont("Helvetica");
            tf12.TextState.FontSize = 12;
            tf12.TextState.ForegroundColor = Color.Blue;
            tf12.TextState.FontStyle = FontStyles.Italic;
            cell12.Paragraphs.Add(tf12);

            // -------------------------
            // Row 2
            // -------------------------
            Row row2 = table.Rows.Add();

            // Cell 1 – underlined green text (use TextState.Underline)
            Cell cell21 = row2.Cells.Add();
            TextFragment tf21 = new TextFragment("Underlined Green");
            tf21.TextState.Font = FontRepository.FindFont("Helvetica");
            tf21.TextState.FontSize = 12;
            tf21.TextState.ForegroundColor = Color.Green;
            tf21.TextState.Underline = true; // Correct way to apply underline
            cell21.Paragraphs.Add(tf21);

            // Cell 2 – mixed formatting (bold + italic, orange)
            Cell cell22 = row2.Cells.Add();
            TextFragment tf22 = new TextFragment("Bold Italic Orange");
            tf22.TextState.Font = FontRepository.FindFont("Helvetica");
            tf22.TextState.FontSize = 12;
            tf22.TextState.ForegroundColor = Color.Orange;
            tf22.TextState.FontStyle = FontStyles.Bold | FontStyles.Italic;
            cell22.Paragraphs.Add(tf22);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rich‑text table saved to '{outputPath}'.");
    }
}
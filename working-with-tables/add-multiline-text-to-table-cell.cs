using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "multiline_cell.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table();
            page.Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // First line of text
            TextFragment line1 = new TextFragment("First line of text");
            line1.TextState.FontSize = 12;
            line1.TextState.Font = FontRepository.FindFont("Helvetica");
            line1.TextState.ForegroundColor = Color.Black;

            // Line break fragment (empty text with a newline)
            TextFragment lineBreak = new TextFragment("\n");

            // Second line of text
            TextFragment line2 = new TextFragment("Second line of text");
            line2.TextState.FontSize = 12;
            line2.TextState.Font = FontRepository.FindFont("Helvetica");
            line2.TextState.ForegroundColor = Color.Black;

            // Third line of text
            TextFragment line3 = new TextFragment("Third line of text");
            line3.TextState.FontSize = 12;
            line3.TextState.Font = FontRepository.FindFont("Helvetica");
            line3.TextState.ForegroundColor = Color.Black;

            // Insert the fragments into the cell, separating them with line‑break fragments
            cell.Paragraphs.Add(line1);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line2);
            cell.Paragraphs.Add(lineBreak);
            cell.Paragraphs.Add(line3);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiline cell saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Work with the first page
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a table
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "30% 70%"
            };

            // Row 1
            Aspose.Pdf.Row row1 = table.Rows.Add();
            // Cell 1
            Aspose.Pdf.Cell cell11 = row1.Cells.Add();
            TextFragment tf11 = new TextFragment("Product");
            tf11.TextState.FontSize = 12;
            tf11.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            cell11.Paragraphs.Add(tf11);
            // Cell 2 with footnote reference superscript ¹
            Aspose.Pdf.Cell cell12 = row1.Cells.Add();
            TextFragment tf12 = new TextFragment("Widget A¹");
            tf12.TextState.FontSize = 12;
            tf12.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            cell12.Paragraphs.Add(tf12);

            // Row 2
            Aspose.Pdf.Row row2 = table.Rows.Add();
            // Cell 1
            Aspose.Pdf.Cell cell21 = row2.Cells.Add();
            TextFragment tf21 = new TextFragment("Price");
            tf21.TextState.FontSize = 12;
            tf21.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            cell21.Paragraphs.Add(tf21);
            // Cell 2 with footnote reference superscript ²
            Aspose.Pdf.Cell cell22 = row2.Cells.Add();
            TextFragment tf22 = new TextFragment("$50²");
            tf22.TextState.FontSize = 12;
            tf22.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            cell22.Paragraphs.Add(tf22);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Add a blank line before footnotes
            page.Paragraphs.Add(new TextFragment(Environment.NewLine));

            // Footnote 1
            TextFragment footnote1 = new TextFragment("¹ This is the first footnote describing Widget A.");
            footnote1.TextState.FontSize = 10;
            footnote1.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            footnote1.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            page.Paragraphs.Add(footnote1);

            // Footnote 2
            TextFragment footnote2 = new TextFragment("² Price includes tax.");
            footnote2.TextState.FontSize = 10;
            footnote2.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica")!; // Suppress possible null warning
            footnote2.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;
            page.Paragraphs.Add(footnote2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table and footnotes saved to '{outputPath}'.");
    }
}
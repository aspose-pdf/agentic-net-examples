using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_repeating_header.pdf";

        // Ensure output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a page (pages are 1‑based)
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Create a table and set the number of header rows to repeat on each page
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                RepeatingRowsCount = 1,          // repeat first row on every page
                ColumnWidths = "100 200 150"    // optional column widths
            };

            // Add header row
            Aspose.Pdf.Row header = table.Rows.Add();
            AddCell(header, "ID", true);
            AddCell(header, "Name", true);
            AddCell(header, "Description", true);

            // Add many data rows to force pagination
            for (int i = 1; i <= 100; i++)
            {
                Aspose.Pdf.Row row = table.Rows.Add();
                AddCell(row, i.ToString(), false);
                AddCell(row, $"Item {i}", false);
                AddCell(row, $"This is a description for item {i}. It may be long enough to wrap onto multiple lines.", false);
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'. Header rows will repeat on each page.");
    }

    // Helper to add a cell with optional header styling
    static void AddCell(Aspose.Pdf.Row row, string text, bool isHeader)
    {
        Aspose.Pdf.Cell cell = row.Cells.Add();
        Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(text);

        if (isHeader)
        {
            tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica-Bold");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            tf.TextState.BackgroundColor = Aspose.Pdf.Color.LightGray;
        }
        else
        {
            tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 10;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
        }

        cell.Paragraphs.Add(tf);
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // existing PDF (can be empty)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we have at least one page to place the tables
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // -----------------------------------------------------------------
            // First table – fit to page width (AutoFitToWindow)
            // -----------------------------------------------------------------
            Table table1 = new Table
            {
                // ColumnAdjustment controls how the table sizes itself
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header1 = table1.Rows.Add();
            header1.Cells.Add("Header A");
            header1.Cells.Add("Header B");
            header1.BackgroundColor = Color.LightGray;

            // Add a data row
            Row data1 = table1.Rows.Add();
            data1.Cells.Add("Row1 Col1");
            data1.Cells.Add("Row1 Col2");

            // Place the first table on the first page
            doc.Pages[1].Paragraphs.Add(table1);

            // -----------------------------------------------------------------
            // Second table – size to content (AutoFitToContent)
            // -----------------------------------------------------------------
            Table table2 = new Table
            {
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a header row
            Row header2 = table2.Rows.Add();
            header2.Cells.Add("Long Header Column 1");
            header2.Cells.Add("Long Header Column 2");
            header2.BackgroundColor = Color.LightBlue;

            // Add a data row with longer text to demonstrate content‑based sizing
            Row data2 = table2.Rows.Add();
            data2.Cells.Add("This is a much longer piece of text that will expand the column width.");
            data2.Cells.Add("Short");

            // Place the second table below the first one (add a blank line first)
            doc.Pages[1].Paragraphs.Add(new TextFragment("\n"));
            doc.Pages[1].Paragraphs.Add(table2);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with varied ColumnAdjustment settings: {outputPath}");
    }
}

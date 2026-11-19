using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure the document has at least one page (1‑based indexing)
                Page page = doc.Pages[1];

                // -------------------------------------------------
                // Table 1 – AutoFit to content (ColumnAdjustment)
                // -------------------------------------------------
                Table table1 = new Table
                {
                    ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                    Border = new BorderInfo(BorderSide.All, 0.5f)
                };

                // Header row
                Row headerRow1 = table1.Rows.Add();
                headerRow1.Cells.Add("Header 1");
                headerRow1.Cells.Add("Header 2");

                // Data row
                Row dataRow1 = table1.Rows.Add();
                dataRow1.Cells.Add("Value A1");
                dataRow1.Cells.Add("Value A2");

                // Add the first table to the page
                page.Paragraphs.Add(table1);

                // -------------------------------------------------
                // Table 2 – AutoFit to window (ColumnAdjustment)
                // -------------------------------------------------
                Table table2 = new Table
                {
                    // AutoFitToWindow makes the table stretch to the page width
                    ColumnAdjustment = ColumnAdjustment.AutoFitToWindow,
                    Border = new BorderInfo(BorderSide.All, 0.5f)
                };

                // Header row
                Row headerRow2 = table2.Rows.Add();
                headerRow2.Cells.Add("Header X");
                headerRow2.Cells.Add("Header Y");

                // Data row
                Row dataRow2 = table2.Rows.Add();
                dataRow2.Cells.Add("Value B1");
                dataRow2.Cells.Add("Value B2");

                // Add a blank line for visual separation
                page.Paragraphs.Add(new TextFragment("\n"));
                // Add the second table to the page
                page.Paragraphs.Add(table2);

                // Save the modified PDF (PDF format, no SaveOptions needed)
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with varied table sizing to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

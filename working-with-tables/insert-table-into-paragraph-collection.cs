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
            using (Document doc = new Document(inputPath))
            {
                // Use the first page (or add a new one if needed)
                Page page = doc.Pages[1];

                // Add introductory text as a TextFragment (Paragraph type does not exist in Aspose.PDF)
                TextFragment intro = new TextFragment("Below is a table:");
                page.Paragraphs.Add(intro);

                // Create a table and define column widths (percentage)
                Table table = new Table
                {
                    ColumnWidths = "50 50"
                };

                // Header row
                Row header = table.Rows.Add();
                header.Cells.Add("Product");
                header.Cells.Add("Price");

                // First data row
                Row row1 = table.Rows.Add();
                row1.Cells.Add("Widget A");
                row1.Cells.Add("$10");

                // Second data row
                Row row2 = table.Rows.Add();
                row2.Cells.Add("Widget B");
                row2.Cells.Add("$20");

                // Add the table to the page after the introductory text
                page.Paragraphs.Add(table);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

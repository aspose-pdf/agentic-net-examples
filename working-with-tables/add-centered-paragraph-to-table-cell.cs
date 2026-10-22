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

        // If the input file does not exist, create a simple empty PDF.
        if (!File.Exists(inputPath))
        {
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
            {
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Open the PDF and add a table with a centered paragraph inside a cell.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Ensure there is at least one page.
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table with a single column.
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                ColumnWidths = "200"
            };

            // Add a row to the table.
            Aspose.Pdf.Row row = table.Rows.Add();

            // Add a cell to the row.
            Aspose.Pdf.Cell cell = row.Cells.Add();

            // Create a text fragment, set its text and center alignment.
            // TextFragment derives from Paragraph, so it can be added to cell.Paragraphs.
            Aspose.Pdf.Text.TextFragment fragment = new Aspose.Pdf.Text.TextFragment("Centered text")
            {
                HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Center
            };

            // Insert the fragment into the cell.
            cell.Paragraphs.Add(fragment);

            // Add the table to the first page of the document.
            doc.Pages[1].Paragraphs.Add(table);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table with centered paragraph saved to '{outputPath}'.");
    }
}

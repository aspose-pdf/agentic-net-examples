using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_autoheight.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF, add a table, and let the row auto‑adjust its height
        using (Document doc = new Document(inputPdf))
        {
            // Add a new page if the document has no pages
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Create a table and add it to the first page
            Table table = new Table
            {
                // Optional: set column widths (example: two equal columns)
                ColumnWidths = "200 200"
            };
            doc.Pages[1].Paragraphs.Add(table);

            // Add a row to the table
            Row row = table.Rows.Add();

            // Set MinRowHeight to 0 so the row height is determined by its content.
            // Aspose.Pdf.Row does not have a Height property; using MinRowHeight = 0
            // enables automatic height adjustment (AutoFit behavior).
            row.MinRowHeight = 0;

            // Add cells with sample content that may require varying heights
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("Short text"));

            Cell cell2 = row.Cells.Add();
            // Long text will cause the row to expand automatically
            string longText = "This is a longer piece of text that should wrap and cause the row height to increase automatically based on its content.";
            cell2.Paragraphs.Add(new TextFragment(longText));

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with auto‑adjusting row height: {outputPdf}");
    }
}
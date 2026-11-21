using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string svgImagePath = "image.svg";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(svgImagePath))
        {
            Console.Error.WriteLine($"SVG image not found: {svgImagePath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a table with two columns
            Table table = new Table { ColumnWidths = "250 250" };

            // Add the first row
            Row row = table.Rows.Add();

            // First cell: simple text
            Cell cell1 = row.Cells.Add();
            cell1.Paragraphs.Add(new TextFragment("First cell text"));

            // Second cell: embed the SVG image
            Cell cell2 = row.Cells.Add();
            // Keep the SVG stream open until the document is saved
            using (FileStream svgStream = File.OpenRead(svgImagePath))
            {
                Image svgImage = new Image { ImageStream = svgStream };
                // Optional: set size if required, e.g., svgImage.FixWidth = 200; svgImage.FixHeight = 200;
                cell2.Paragraphs.Add(svgImage);

                // Insert the table into the first page (1‑based indexing)
                Page firstPage = doc.Pages[1];
                firstPage.Paragraphs.Add(table);

                // Save the modified PDF while the stream is still alive
                doc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"PDF with table saved to '{outputPdfPath}'.");
    }
}

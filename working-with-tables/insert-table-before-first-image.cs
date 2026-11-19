using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertTableBeforeImage
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate through all pages
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageIndex];

                // Check if the page contains any images
                if (page.Resources.Images.Count > 0)
                {
                    // Create a table
                    Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                    // Define two columns with equal width
                    table.ColumnWidths = "200 200";

                    // Header row
                    Aspose.Pdf.Row headerRow = table.Rows.Add();
                    Aspose.Pdf.Cell headerCell1 = headerRow.Cells.Add();
                    headerCell1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Column 1"));
                    Aspose.Pdf.Cell headerCell2 = headerRow.Cells.Add();
                    headerCell2.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Column 2"));

                    // Data row
                    Aspose.Pdf.Row dataRow = table.Rows.Add();
                    Aspose.Pdf.Cell dataCell1 = dataRow.Cells.Add();
                    dataCell1.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Value A"));
                    Aspose.Pdf.Cell dataCell2 = dataRow.Cells.Add();
                    dataCell2.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Value B"));

                    // Insert the table at the beginning of the page's content stream
                    // This places the table before any existing content, including the image
                    page.Paragraphs.Insert(0, table);

                    // Only insert before the first image found; break after handling this page
                    break;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted before image and saved to '{outputPath}'.");
    }
}
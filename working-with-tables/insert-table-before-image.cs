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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];

                // ParagraphCollection is 1‑based. Scan for Image objects.
                for (int i = 1; i <= page.Paragraphs.Count; i++)
                {
                    // The concrete type for an image placed on a page is Aspose.Pdf.Image
                    if (page.Paragraphs[i] is Image)
                    {
                        // Build a simple table to insert before the image
                        Table table = new Table
                        {
                            // Optional visual settings
                            Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                            DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray),
                            DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
                        };

                        // Define two columns (adjust widths as needed)
                        table.ColumnWidths = "200 200";

                        // Header row
                        Row header = table.Rows.Add();
                        header.Cells.Add("Column 1");
                        header.Cells.Add("Column 2");
                        // Apply a simple background to header cells
                        foreach (Cell cell in header.Cells)
                        {
                            cell.BackgroundColor = Aspose.Pdf.Color.LightGray;
                            cell.DefaultCellTextState = new TextState
                            {
                                FontSize = 12,
                                FontStyle = FontStyles.Bold,
                                Font = FontRepository.FindFont("Helvetica")
                            };
                        }

                        // Data row
                        Row data = table.Rows.Add();
                        data.Cells.Add("Value A");
                        data.Cells.Add("Value B");

                        // Insert the table before the image (at the same index)
                        page.Paragraphs.Insert(i, table);

                        // If there may be multiple images and you only want the first,
                        // break out of the inner loop after insertion.
                        // break;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table inserted before images and saved to '{outputPath}'.");
    }
}
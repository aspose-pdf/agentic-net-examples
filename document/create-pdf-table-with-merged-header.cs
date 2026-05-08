using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "merged_header_table.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and configure basic appearance
            Table table = new Table
            {
                // Three columns, each 150 points wide
                ColumnWidths = "150 150 150",
                // Optional: border around the whole table
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black),
                // Optional: default padding inside cells
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // -------------------------------------------------
            // Header row with a single cell that spans all columns
            // -------------------------------------------------
            Row mergedHeaderRow = table.Rows.Add();

            // Create the cell that will span the three columns
            Cell mergedHeaderCell = mergedHeaderRow.Cells.Add();
            mergedHeaderCell.ColSpan = 3; // merge across three columns
            mergedHeaderCell.BackgroundColor = Aspose.Pdf.Color.LightGray;
            mergedHeaderCell.Alignment = HorizontalAlignment.Center;
            mergedHeaderCell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 14,
                ForegroundColor = Aspose.Pdf.Color.Black
            };
            mergedHeaderCell.Paragraphs.Add(new TextFragment("Sales Report 2023"));

            // -------------------------------------------------
            // Second header row with individual column titles
            // -------------------------------------------------
            Row columnHeaderRow = table.Rows.Add();

            Cell productHeader = columnHeaderRow.Cells.Add();
            productHeader.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            productHeader.Alignment = HorizontalAlignment.Center;
            productHeader.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            productHeader.Paragraphs.Add(new TextFragment("Product"));

            Cell quantityHeader = columnHeaderRow.Cells.Add();
            quantityHeader.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            quantityHeader.Alignment = HorizontalAlignment.Center;
            quantityHeader.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            quantityHeader.Paragraphs.Add(new TextFragment("Quantity"));

            Cell priceHeader = columnHeaderRow.Cells.Add();
            priceHeader.BackgroundColor = Aspose.Pdf.Color.LightBlue;
            priceHeader.Alignment = HorizontalAlignment.Center;
            priceHeader.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Bold"),
                FontSize = 12,
                ForegroundColor = Aspose.Pdf.Color.White
            };
            priceHeader.Paragraphs.Add(new TextFragment("Price"));

            // -------------------------------------------------
            // Data rows
            // -------------------------------------------------
            string[,] data = {
                { "Widget A", "120", "$5.00" },
                { "Widget B", "80",  "$7.50" },
                { "Widget C", "150", "$3.20" }
            };

            for (int i = 0; i < data.GetLength(0); i++)
            {
                Row dataRow = table.Rows.Add();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    Cell dataCell = dataRow.Cells.Add();
                    dataCell.Alignment = HorizontalAlignment.Center;
                    dataCell.DefaultCellTextState = new TextState
                    {
                        Font = FontRepository.FindFont("Helvetica"),
                        FontSize = 11,
                        ForegroundColor = Aspose.Pdf.Color.Black
                    };
                    dataCell.Paragraphs.Add(new TextFragment(data[i, j]));
                }
            }

            // Add the table to the page
            page.Paragraphs.Add(table);

            // -------------------------------------------------
            // Save the PDF – guard against missing GDI+ on non‑Windows platforms
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF created: {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF created (non‑Windows platform): {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved using Aspose.Pdf's default renderer.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}

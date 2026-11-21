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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 10 pages (Aspose.Pdf uses 1‑based indexing).
            if (doc.Pages.Count < 10)
            {
                Console.Error.WriteLine("The document does not contain page 10.");
                return;
            }

            // Get page 10.
            Page page = doc.Pages[10];

            // Use TableAbsorber to locate existing tables on the page.
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page);

            // If a table was found, replace the first one with a new table.
            if (absorber.TableList.Count > 0)
            {
                // Build the replacement table.
                Table newTable = new Table
                {
                    // Define column widths (example: two equal columns).
                    ColumnWidths = "250 250",
                    // Optional visual styling.
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                    DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
                };

                // Header row.
                Row header = newTable.Rows.Add();
                Cell headerCell1 = header.Cells.Add("Header 1");
                Cell headerCell2 = header.Cells.Add("Header 2");
                headerCell1.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
                headerCell2.DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };

                // Data row.
                Row dataRow = newTable.Rows.Add();
                dataRow.Cells.Add("Value A");
                dataRow.Cells.Add("Value B");

                // Replace the first absorbed table with the new one, preserving its position.
                AbsorbedTable absorbed = absorber.TableList[0];
                absorber.Replace(page, absorbed, newTable);
            }
            else
            {
                // No existing table – simply add the new table to the page.
                Table newTable = new Table
                {
                    ColumnWidths = "250 250",
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.Black),
                    DefaultCellPadding = new MarginInfo(4, 4, 4, 4)
                };
                Row header = newTable.Rows.Add();
                header.Cells.Add("Header 1").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
                header.Cells.Add("Header 2").DefaultCellTextState = new TextState { FontStyle = FontStyles.Bold };
                Row dataRow = newTable.Rows.Add();
                dataRow.Cells.Add("Value A");
                dataRow.Cells.Add("Value B");
                page.Paragraphs.Add(newTable);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

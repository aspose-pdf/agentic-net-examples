using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 30 pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 30)
            {
                Console.Error.WriteLine("The document does not contain page 30.");
                return;
            }

            // Get page 30
            Page page30 = doc.Pages[30];

            // Use TableAbsorber to extract tables from the page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(page30);

            // If no tables were found, report and exit
            if (absorber.TableList.Count == 0)
            {
                Console.Error.WriteLine("No tables were detected on page 30.");
                return;
            }

            // Retrieve the first absorbed table (as an example)
            AbsorbedTable absorbedTable = absorber.TableList[0];

            // ------------------------------------------------------------
            // Build a new Table that will replace (or be added next to) the
            // absorbed one. The Table API is used for creating a visual
            // table in the PDF – AbsorbedTable is only a read‑only model.
            // ------------------------------------------------------------
            Table newTable = new Table
            {
                // Position the new table at the same location as the absorbed one
                // Aspose.Pdf uses lower‑left origin; Top is the Y coordinate of the
                // upper edge (URY) of the rectangle.
                Left = (float)absorbedTable.Rectangle.LLX,
                Top = (float)absorbedTable.Rectangle.URY,
                // Define column widths – adjust as needed
                ColumnWidths = "200 200"
            };

            // Optional: give the table a thin border for visibility
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f);

            // Add a row with two cells
            Row row = newTable.Rows.Add();
            Cell cell1 = row.Cells.Add("New Cell 1");
            cell1.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };
            Cell cell2 = row.Cells.Add("New Cell 2");
            cell2.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 12,
                ForegroundColor = Color.Black
            };

            // Insert the new table into the page
            page30.Paragraphs.Add(newTable);

            // (Optional) Remove the original table that was absorbed
            // absorber.Remove(absorbedTable);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

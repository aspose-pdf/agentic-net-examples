using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableToCsvExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedTables";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (creation + loading)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TableAbsorber instance (object creation)
            TableAbsorber absorber = new TableAbsorber();

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Extract tables on the current page
                absorber.Visit(page);

                // Process each found table
                for (int tableIdx = 0; tableIdx < absorber.TableList.Count; tableIdx++)
                {
                    var absorbedTable = absorber.TableList[tableIdx];

                    // Build CSV content for the current table
                    StringBuilder csvBuilder = new StringBuilder();

                    foreach (var row in absorbedTable.RowList)
                    {
                        // Collect cell texts for the current row
                        var cellTexts = new string[row.CellList.Count];
                        for (int cellIdx = 0; cellIdx < row.CellList.Count; cellIdx++)
                        {
                            var cell = row.CellList[cellIdx];
                            // Concatenate all text fragments inside the cell
                            StringBuilder cellBuilder = new StringBuilder();
                            foreach (var fragment in cell.TextFragments)
                            {
                                // Trim to avoid unwanted line breaks
                                cellBuilder.Append(fragment.Text.Trim());
                                cellBuilder.Append(' ');
                            }
                            // Remove trailing space
                            cellTexts[cellIdx] = cellBuilder.ToString().Trim();
                        }

                        // Join cell texts with commas and add a new line
                        csvBuilder.AppendLine(string.Join(",", cellTexts));
                    }

                    // Determine CSV file name: table_{page}_{index}.csv
                    string csvFileName = $"table_page{pageIndex}_tbl{tableIdx + 1}.csv";
                    string csvPath = Path.Combine(outputFolder, csvFileName);

                    // Write CSV content to file (standard .NET I/O, not Aspose.Pdf.Save)
                    File.WriteAllText(csvPath, csvBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine($"Extracted table saved to: {csvPath}");
                }

                // Clear the absorber for the next page to avoid mixing results
                absorber.TableList.Clear();
            }
        }
    }
}
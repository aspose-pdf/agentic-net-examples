using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputCsvPath = "output.csv";

        // ------------------------------------------------------------
        // Ensure a sample PDF exists – the sandbox does not contain any files.
        // Create a one‑page PDF with a simple table so that TableAbsorber has
        // something to extract.
        // ------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                Page page = placeholder.Pages.Add();

                // Create a table with two columns and two rows.
                Table sampleTable = new Table
                {
                    // Define column widths (in points). Adjust as needed.
                    ColumnWidths = "150 150"
                };

                // Header row
                Row header = sampleTable.Rows.Add();
                header.Cells.Add("Header 1");
                header.Cells.Add("Header 2");

                // Data row
                Row data = sampleTable.Rows.Add();
                data.Cells.Add("Cell 1");
                data.Cells.Add("Cell 2");

                // Add the table to the page.
                page.Paragraphs.Add(sampleTable);

                // Save the placeholder PDF.
                placeholder.Save(inputPdfPath);
            }
        }

        // ------------------------------------------------------------
        // Load the PDF document (lifecycle rule: use Document constructor)
        // ------------------------------------------------------------
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Prepare a writer for the CSV output
            using (StreamWriter csvWriter = new StreamWriter(outputCsvPath))
            {
                // Aspose.Pdf evaluation mode allows a maximum of 4 pages.
                // Limit the loop to 4 pages to avoid IndexOutOfRangeException in eval mode.
                int maxPages = Math.Min(pdfDocument.Pages.Count, 4);
                for (int pageIndex = 1; pageIndex <= maxPages; pageIndex++)
                {
                    Page page = pdfDocument.Pages[pageIndex];

                    // TableAbsorber extracts tables from a page.
                    // Setting UseFlowEngine = true makes BorderInfo available for each cell.
                    TableAbsorber tableAbsorber = new TableAbsorber
                    {
                        UseFlowEngine = true
                    };
                    tableAbsorber.Visit(page);

                    // Process each extracted table
                    foreach (var table in tableAbsorber.TableList)
                    {
                        // TableAbsorber.Table.RowList is a collection of rows.
                        // Each row contains a CellList collection.
                        foreach (var row in table.RowList)
                        {
                            // Start each CSV line with a visual border marker.
                            csvWriter.Write("|");

                            // Process each cell in the current row.
                            foreach (var cell in row.CellList)
                            {
                                // Retrieve the textual content of the cell.
                                string cellText = string.Empty;
                                if (cell.TextFragments != null && cell.TextFragments.Count > 0)
                                {
                                    // TextFragmentCollection is 1‑based indexed.
                                    cellText = cell.TextFragments[1].Text;
                                }

                                // Escape any existing delimiters (commas) in the text.
                                if (cellText.Contains(","))
                                    cellText = $"\"{cellText}\"";

                                // Write the cell content.
                                csvWriter.Write(cellText);

                                // Add a delimiter marker that represents the right border of the cell.
                                // The pipe character is used as a visual border indicator.
                                csvWriter.Write("|");
                            }

                            // End of the CSV line.
                            csvWriter.WriteLine();
                        }

                        // Add an empty line between tables for readability.
                        csvWriter.WriteLine();
                    }
                }
            }
        }

        Console.WriteLine($"CSV export completed: {outputCsvPath}");
    }
}

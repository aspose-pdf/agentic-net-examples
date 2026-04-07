using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace BatchReplaceTablesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of PDF files to process (adjust paths as needed)
            string[] inputFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };

            foreach (string inputPath in inputFiles)
            {
                // Ensure the file exists before processing
                if (!File.Exists(inputPath))
                {
                    Console.WriteLine($"File not found: {inputPath}");
                    continue;
                }

                // Open the PDF document inside a using block for deterministic disposal
                using (Document doc = new Document(inputPath))
                {
                    // Iterate through all pages (1‑based indexing)
                    for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                    {
                        Page page = doc.Pages[pageNumber];

                        // Create a TableAbsorber to find tables on the current page
                        TableAbsorber absorber = new TableAbsorber();
                        absorber.Visit(page);

                        // Copy the TableList to avoid collection modification issues
                        List<AbsorbedTable> tablesCopy = new List<AbsorbedTable>(absorber.TableList);

                        // Process at most four tables per page (evaluation‑mode limitation)
                        int processedCount = 0;
                        foreach (AbsorbedTable oldTable in tablesCopy)
                        {
                            if (processedCount >= 4)
                                break;

                            // Determine the number of rows and columns of the absorbed table
                            int rowCount = oldTable.RowList.Count;
                            int columnCount = 0;
                            if (rowCount > 0)
                                columnCount = oldTable.RowList[0].CellList.Count;

                            // Create a new Table with the same dimensions
                            Table newTable = new Table();

                            // Set equal column widths (example: 50 units each)
                            string columnWidths = "";
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnWidths += "50 ";
                            }
                            newTable.ColumnWidths = columnWidths.Trim();

                            // Populate the new table with placeholder text
                            for (int r = 0; r < rowCount; r++)
                            {
                                Row newRow = newTable.Rows.Add();
                                for (int c = 0; c < columnCount; c++)
                                {
                                    Cell newCell = newRow.Cells.Add("New");
                                }
                            }

                            // Replace the old absorbed table with the newly created table
                            absorber.Replace(page, oldTable, newTable);

                            processedCount++;
                        }
                    }

                    // Save the modified document – output file name is simple (no directory path)
                    string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_updated.pdf";
                    doc.Save(outputFileName);
                    Console.WriteLine($"Processed '{inputPath}' and saved as '{outputFileName}'.");
                }
            }
        }
    }
}

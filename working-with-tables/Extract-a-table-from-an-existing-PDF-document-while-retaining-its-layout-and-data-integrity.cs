using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted_table.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber which will locate tables in the document
            TableAbsorber absorber = new TableAbsorber();

            // Extract tables from the whole document (all pages)
            absorber.Visit(doc);

            // If no tables were found, inform the user and exit
            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables were detected in the document.");
                return;
            }

            // Write the extracted table data to a text file.
            // Tab characters are used to keep a simple column layout.
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                int tableIndex = 1;
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    writer.WriteLine($"--- Table {tableIndex} (Page {table.PageNum}) ---");

                    // Iterate through each row of the absorbed table
                    foreach (var row in table.RowList)
                    {
                        // Collect text from each cell in the current row
                        string[] cellTexts = new string[row.CellList.Count];
                        for (int c = 0; c < row.CellList.Count; c++)
                        {
                            // A cell may contain multiple text fragments; concatenate them
                            TextFragmentCollection fragments = row.CellList[c].TextFragments;
                            string cellText = string.Empty;
                            foreach (TextFragment frag in fragments)
                            {
                                cellText += frag.Text;
                            }
                            cellTexts[c] = cellText;
                        }

                        // Write the row as a tab‑separated line
                        writer.WriteLine(string.Join("\t", cellTexts));
                    }

                    writer.WriteLine(); // Blank line between tables
                    tableIndex++;
                }
            }

            Console.WriteLine($"Extracted tables saved to '{outputPath}'.");
        }
    }
}
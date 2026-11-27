using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPdf = "input.pdf";
        // Output plain‑text file path
        const string outputTxt = "tables_report.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Use TableAbsorber (core API) to locate tables. This provides the page number via AbsorbedTable.PageNum.
            var absorber = new TableAbsorber();
            absorber.Visit(doc);

            // Prepare the output file
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                // Iterate over each absorbed table found in the document
                foreach (AbsorbedTable absorbedTable in absorber.TableList)
                {
                    int pageNumber = absorbedTable.PageNum; // Correct property for page number

                    // Each AbsorbedTable contains a list of rows (AbsorbedRow)
                    foreach (AbsorbedRow row in absorbedTable.RowList)
                    {
                        List<string> cellTexts = new List<string>();

                        // Each row contains a list of cells (AbsorbedCell)
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            // A cell may contain multiple TextFragments; concatenate them.
                            string cellText = string.Empty;
                            if (cell.TextFragments != null && cell.TextFragments.Count > 0)
                            {
                                cellText = string.Join("", cell.TextFragments.Select(tf => tf.Text));
                            }
                            cellTexts.Add(cellText);
                        }

                        // Write the row information: page number followed by cell contents
                        writer.WriteLine($"Page {pageNumber}: {string.Join(" | ", cellTexts)}");
                    }
                }
            }

            Console.WriteLine($"Table extraction completed. Report saved to '{outputTxt}'.");
        }
    }
}

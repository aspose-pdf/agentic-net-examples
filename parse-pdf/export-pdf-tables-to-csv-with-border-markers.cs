using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ExportTableWithBorders
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputCsv = "tables_with_borders.csv";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Absorb tables from the document
            TableAbsorber tableAbsorber = new TableAbsorber();
            foreach (Page page in pdfDoc.Pages)
            {
                tableAbsorber.Visit(page);
            }

            // Prepare CSV writer
            using (StreamWriter writer = new StreamWriter(outputCsv))
            {
                int tableIndex = 1;
                foreach (var table in tableAbsorber.TableList)
                {
                    writer.WriteLine($"# Table {tableIndex}");
                    writer.WriteLine(new string('=', 50));

                    foreach (var row in table.RowList)
                    {
                        // Visual separator for a top border – check the first cell of the row
                        var firstCell = row.CellList.Count > 0 ? row.CellList[0] : null;
                        if (firstCell?.BorderInfo?.Top != null)
                        {
                            writer.WriteLine(new string('-', 50));
                        }

                        // Build CSV line with cell delimiters
                        string[] cellTexts = new string[row.CellList.Count];
                        for (int i = 0; i < row.CellList.Count; i++)
                        {
                            var cell = row.CellList[i];
                            // Concatenate all text fragments inside the cell
                            string cellText = string.Empty;
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText += fragment.Text;
                            }

                            // Trim whitespace and escape pipe delimiters
                            cellText = cellText.Trim().Replace("|", "&#124;");

                            // Add visual marker if the cell has left/right borders
                            bool hasLeftBorder = cell.BorderInfo?.Left != null;
                            bool hasRightBorder = cell.BorderInfo?.Right != null;

                            if (hasLeftBorder) cellText = $"|{cellText}";
                            if (hasRightBorder) cellText = $"{cellText}|";

                            cellTexts[i] = cellText;
                        }

                        // Join cells with a pipe delimiter for visual reference
                        writer.WriteLine(string.Join("|", cellTexts));

                        // Visual separator for a bottom border – check the first cell of the row
                        if (firstCell?.BorderInfo?.Bottom != null)
                        {
                            writer.WriteLine(new string('-', 50));
                        }
                    }

                    writer.WriteLine(); // Blank line between tables
                    tableIndex++;
                }
            }

            Console.WriteLine($"Tables exported with border markers to '{outputCsv}'.");
        }
    }
}

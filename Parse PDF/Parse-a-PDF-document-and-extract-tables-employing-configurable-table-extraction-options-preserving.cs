using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class TableExtractionExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TableAbsorber instance
            TableAbsorber absorber = new TableAbsorber();

            // Optional: enable the flow engine to improve recognition (preserves formatting)
            absorber.UseFlowEngine = true;

            // Extract tables from the entire document
            absorber.Visit(doc);

            // Iterate over all found tables
            for (int t = 0; t < absorber.TableList.Count; t++)
            {
                var table = absorber.TableList[t];
                Console.WriteLine($"Table {t + 1}:");

                // Iterate over rows
                for (int r = 0; r < table.RowList.Count; r++)
                {
                    var row = table.RowList[r];
                    Console.Write($"  Row {r + 1}: ");

                    // Iterate over cells
                    for (int c = 0; c < row.CellList.Count; c++)
                    {
                        var cell = row.CellList[c];

                        // Concatenate all text fragments in the cell
                        string cellText = "";
                        foreach (TextFragment fragment in cell.TextFragments)
                        {
                            // Preserve formatting information
                            string fontName = fragment.TextState.Font?.FontName ?? "Default";
                            double fontSize = fragment.TextState.FontSize;
                            Aspose.Pdf.Color color = fragment.TextState.ForegroundColor;

                            // Append formatted representation
                            cellText += $"[Text=\"{fragment.Text}\", Font=\"{fontName}\", Size={fontSize}, Color={color}] ";
                        }

                        Console.Write(cellText);
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table_cell.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Locate tables on the first page
                TableAbsorber tableAbsorber = new TableAbsorber();
                tableAbsorber.Visit(doc.Pages[1]);

                // Verify that a table, row, cell, and text fragment exist
                if (tableAbsorber.TableList.Count > 0 &&
                    tableAbsorber.TableList[0].RowList.Count > 0 &&
                    tableAbsorber.TableList[0].RowList[0].CellList.Count > 0 &&
                    tableAbsorber.TableList[0].RowList[0].CellList[0].TextFragments.Count > 0)
                {
                    // Get the first text fragment in the first cell
                    TextFragment fragment = tableAbsorber.TableList[0]
                        .RowList[0]
                        .CellList[0]
                        .TextFragments[0];

                    // Rotate the text by 60 degrees
                    fragment.TextState.Rotation = 60;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
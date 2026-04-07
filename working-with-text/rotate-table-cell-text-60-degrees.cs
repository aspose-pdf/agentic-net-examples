using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_table.pdf";

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

                if (tableAbsorber.TableList.Count == 0)
                {
                    Console.WriteLine("No tables found on page 1.");
                }
                else
                {
                    // Access the first cell of the first table (adjust indices as needed)
                    var cell = tableAbsorber.TableList[0].RowList[0].CellList[0];

                    // Rotate each text fragment inside the cell by 60 degrees
                    foreach (TextFragment fragment in cell.TextFragments)
                    {
                        fragment.TextState.Rotation = 60; // Rotation in degrees
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
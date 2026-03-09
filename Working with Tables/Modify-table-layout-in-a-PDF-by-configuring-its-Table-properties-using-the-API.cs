using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "modified_table.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Find tables on the first page
            TableAbsorber absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]); // page indexing is 1‑based (rule)

            if (absorber.TableList.Count == 0)
            {
                Console.WriteLine("No tables found on the first page.");
                doc.Save(outputPath); // save unchanged document
                return;
            }

            // Get the first detected table
            AbsorbedTable oldTable = absorber.TableList[0];

            // Create a new Table and configure its layout properties
            Table newTable = new Table();

            // Example layout changes
            newTable.Alignment = HorizontalAlignment.Center;                     // center the table
            newTable.BackgroundColor = Color.LightGray;                         // light gray background
            newTable.Border = new BorderInfo(BorderSide.All, 1.0f, Color.Black); // solid black border, 1 pt
            newTable.DefaultCellPadding = new MarginInfo(5, 5, 5, 5);           // padding inside each cell
            newTable.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Color.DarkGray);
            // In newer Aspose.PDF versions the enum value is AutoFitToContent
            newTable.ColumnAdjustment = ColumnAdjustment.AutoFitToContent;      // auto‑fit columns
            newTable.IsBordersIncluded = true;                                  // include borders in column widths

            // Define column widths (example: two columns, 150 and 250 points)
            // ColumnWidths is a string where widths are space‑ or comma‑separated
            newTable.ColumnWidths = "150 250";

            // OPTIONAL: copy the data from the old table into the new one.
            // Here we flatten all cell texts into a one‑dimensional list and import it.
            var cellTexts = new List<string>();
            foreach (var row in oldTable.RowList)
            {
                foreach (var cell in row.CellList)
                {
                    // Each cell may contain multiple TextFragments; concatenate them.
                    string cellText = string.Empty;
                    foreach (var fragment in cell.TextFragments)
                        cellText += fragment.Text;
                    cellTexts.Add(cellText);
                }
            }

            // ImportArray overload in this Aspose.PDF version expects a 1‑D array.
            // Convert the list to a 1‑D object array and import it.
            object[] data = cellTexts.ToArray();
            newTable.ImportArray(data, 0, 0, false);

            // Replace the old absorbed table with the newly configured table
            absorber.Replace(doc.Pages[1], oldTable, newTable);

            // Save the modified document (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Table layout modified and saved to '{outputPath}'.");
    }
}

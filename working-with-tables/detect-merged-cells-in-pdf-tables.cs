using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
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
            // TableAbsorber extracts tables from the document – it does NOT implement IDisposable
            TableAbsorber absorber = new TableAbsorber();

            // Visit each page to collect tables
            foreach (Page page in doc.Pages)
            {
                absorber.Visit(page);
            }

            int tableIndex = 1;
            foreach (var table in absorber.TableList)
            {
                bool hasMerged = false;
                int rowIndex = 1;

                // Iterate through rows and cells to check RowSpan and ColumnSpan
                foreach (var row in table.RowList)
                {
                    int colIndex = 1;
                    foreach (var cell in row.CellList)
                    {
                        // Use reflection to obtain RowSpan / ColumnSpan – these members may not be present in older library versions
                        int rowSpan = GetIntProperty(cell, "RowSpan", 1);
                        int colSpan = GetIntProperty(cell, "ColumnSpan", 1);

                        if (rowSpan > 1 || colSpan > 1)
                        {
                            hasMerged = true;
                            Console.WriteLine(
                                $"Table {tableIndex}, Cell at Row {rowIndex}, Column {colIndex} " +
                                $"has RowSpan={rowSpan}, ColumnSpan={colSpan}");
                        }
                        colIndex++;
                    }
                    rowIndex++;
                }

                if (!hasMerged)
                {
                    Console.WriteLine($"Table {tableIndex} contains no merged cells.");
                }

                tableIndex++;
            }
        }
    }

    /// <summary>
    /// Safely retrieves an integer property value via reflection.
    /// Returns <paramref name="defaultValue"/> if the property does not exist or cannot be read.
    /// </summary>
    private static int GetIntProperty(object obj, string propertyName, int defaultValue)
    {
        var prop = obj.GetType().GetProperty(propertyName);
        if (prop != null && prop.PropertyType == typeof(int))
        {
            try
            {
                return (int)prop.GetValue(obj);
            }
            catch
            {
                // ignore and fall back to default
            }
        }
        return defaultValue;
    }
}

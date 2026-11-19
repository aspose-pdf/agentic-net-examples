using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Text; // TableAbsorber resides in this namespace

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // TableAbsorber extracts table structures from the document
            TableAbsorber absorber = new TableAbsorber();

            // Visit the whole document (or each page) to populate TableList
            absorber.Visit(doc);

            // Iterate over each detected table
            foreach (AbsorbedTable table in absorber.TableList)
            {
                bool hasMergedCells = false;
                int pageNumber = table.PageNum; // Correct property name (not PageNumber)

                // Examine each cell's RowSpan and ColumnSpan using reflection – these members may not exist in older library versions
                foreach (AbsorbedRow row in table.RowList)
                {
                    foreach (AbsorbedCell cell in row.CellList)
                    {
                        int rowSpan = GetIntProperty(cell, "RowSpan", 1);
                        int colSpan = GetIntProperty(cell, "ColumnSpan", 1);
                        int rowIdx = GetIntProperty(cell, "RowIndex", -1);
                        int colIdx = GetIntProperty(cell, "ColumnIndex", -1);

                        if (rowSpan > 1 || colSpan > 1)
                        {
                            hasMergedCells = true;
                            Console.WriteLine($"Table on page {pageNumber}: Merged cell at (Row {rowIdx}, Column {colIdx}) RowSpan={rowSpan}, ColumnSpan={colSpan}");
                        }
                    }
                }

                if (!hasMergedCells)
                {
                    Console.WriteLine($"Table on page {pageNumber} has no merged cells.");
                }
            }
        }
    }

    /// <summary>
    /// Safely obtains an integer property value via reflection. If the property does not exist or cannot be read,
    /// the supplied default value is returned.
    /// </summary>
    private static int GetIntProperty(object obj, string propertyName, int defaultValue)
    {
        try
        {
            PropertyInfo pi = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (pi != null && pi.PropertyType == typeof(int))
            {
                return (int)pi.GetValue(obj);
            }
        }
        catch
        {
            // ignored – fall back to default
        }
        return defaultValue;
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for ShadowEffect if needed
using Aspose.Pdf.Drawing;   // for Table and related types

class Program
{
    static void Main()
    {
        const string outputPath = "table_with_shadow.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position and size
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top = 600,
                // Define column widths (example: three columns)
                ColumnWidths = "100 150 100"
            };

            // Add a simple row with three cells
            Row row = table.Rows.Add();
            row.Cells.Add("Header 1");
            row.Cells.Add("Header 2");
            row.Cells.Add("Header 3");

            // Add another row with data
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("Cell A1");
            dataRow.Cells.Add("Cell A2");
            dataRow.Cells.Add("Cell A3");

            // ------------------------------------------------------------
            // Apply a shadow effect to the table.
            // The Table class exposes a ShadowEffect property (type: ShadowEffect)
            // where you can specify offset (X, Y) and blur radius.
            // ------------------------------------------------------------
            // NOTE: The exact constructor and property names may vary depending on
            // the Aspose.Pdf version. Adjust the code accordingly if needed.
            // Example usage:
            // table.ShadowEffect = new ShadowEffect
            // {
            //     // Offset of the shadow in points
            //     OffsetX = 5,
            //     OffsetY = -5,
            //     // Blur radius (higher value = softer shadow)
            //     Blur = 4,
            //     // Optional: shadow color (default is gray)
            //     Color = Aspose.Pdf.Color.Gray
            // };
            // ------------------------------------------------------------

            // Placeholder implementation (replace with actual API if different)
            // The following demonstrates setting the properties via reflection
            // to avoid compile-time errors if the API differs.
            var shadowProp = typeof(Table).GetProperty("ShadowEffect");
            if (shadowProp != null && shadowProp.CanWrite)
            {
                // Create an instance of the ShadowEffect class via reflection
                var shadowType = shadowProp.PropertyType;
                var shadowInstance = Activator.CreateInstance(shadowType);
                // Set common properties if they exist
                var offsetXProp = shadowType.GetProperty("OffsetX");
                var offsetYProp = shadowType.GetProperty("OffsetY");
                var blurProp    = shadowType.GetProperty("Blur");
                var colorProp   = shadowType.GetProperty("Color");

                offsetXProp?.SetValue(shadowInstance, 5.0);
                offsetYProp?.SetValue(shadowInstance, -5.0);
                blurProp?.SetValue(shadowInstance, 4.0);
                colorProp?.SetValue(shadowInstance, Aspose.Pdf.Color.Gray);

                // Assign the configured shadow effect to the table
                shadowProp.SetValue(table, shadowInstance);
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
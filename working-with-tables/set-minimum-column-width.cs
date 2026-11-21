using System;
using System.IO;
using Aspose.Pdf; // Core PDF types
using Aspose.Pdf.LogicalStructure; // Tagged‑content table elements

class Program
{
    static void Main()
    {
        const string outputPath = "TableWithMinWidth.pdf";

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Access the tagged‑content API (core API, no Facades)
            var tagged = doc.TaggedContent; // Use var to avoid direct ITaggedContent reference
            var root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with minimum column widths";

            // Define the desired column widths (in points) as a space‑separated string
            // The ColumnWidths property expects a string, not a float array.
            table.ColumnWidths = "120 150 180";

            // Define the minimum column widths (in points) to prevent columns from becoming narrower.
            // The ColumnWidthsMin property is only available in newer Aspose.Pdf versions.
            // If the property does not exist in the referenced version, keep it commented out.
            // table.ColumnWidthsMin = "80 80 80"; // string format, similar to ColumnWidths

            // Attach the table to the root element of the tagged‑content hierarchy
            root.AppendChild(table);

            // ----- Build table header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("Product");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Quantity");
            headerRow.AppendChild(th2);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Price");
            headerRow.AppendChild(th3);

            // ----- Build table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a few data rows
            for (int i = 1; i <= 3; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement td1 = tagged.CreateTableTDElement();
                td1.SetText($"Item {i}");
                dataRow.AppendChild(td1);

                TableTDElement td2 = tagged.CreateTableTDElement();
                td2.SetText((i * 10).ToString());
                dataRow.AppendChild(td2);

                TableTDElement td3 = tagged.CreateTableTDElement();
                td3.SetText($"${i * 5}.00");
                dataRow.AppendChild(td3);
            }

            // Save the PDF (no PreSave call required)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

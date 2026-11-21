using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string outputPath = "conditional_table.pdf";
        const double threshold = 50.0; // cells with value > 50 will be highlighted

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (required for the document to have a page collection)
            doc.Pages.Add();

            // Access the tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Conditional Formatting Example");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with conditional formatting";
            root.AppendChild(table);

            // Create table header
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Header cells
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Item");
            headerRow.AppendChild(thName);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            headerRow.AppendChild(thValue);

            // Create table body
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Sample data rows
            string[] items = { "A", "B", "C", "D", "E" };
            double[] values = { 30, 75, 45, 90, 20 };

            for (int i = 0; i < items.Length; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                // Item cell
                TableTDElement tdItem = tagged.CreateTableTDElement();
                tdItem.SetText(items[i]);
                dataRow.AppendChild(tdItem);

                // Value cell
                TableTDElement tdValue = tagged.CreateTableTDElement();
                tdValue.SetText(values[i].ToString());
                dataRow.AppendChild(tdValue);
            }

            // Apply conditional formatting: set background color for cells where value > threshold
            // Iterate over all TableTDElement cells in the table body
            foreach (Element child in tbody.ChildElements)
            {
                if (child is TableTRElement row)
                {
                    // The second cell in each row holds the numeric value
                    if (row.ChildElements.Count >= 2 && row.ChildElements[1] is TableTDElement valueCell)
                    {
                        if (double.TryParse(valueCell.ActualText, out double cellValue) && cellValue > threshold)
                        {
                            // Set a light red background for cells exceeding the threshold
                            valueCell.BackgroundColor = Aspose.Pdf.Color.FromRgb(1.0, 0.8, 0.8);
                        }
                    }
                }
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional formatting saved to '{outputPath}'.");
    }
}
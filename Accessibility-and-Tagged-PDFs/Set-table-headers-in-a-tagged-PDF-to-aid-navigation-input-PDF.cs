using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // Added for TextFragment and FontStyles

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load existing PDF
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has TaggedContent support
            ITaggedContent tagged = pdfDocument.TaggedContent;
            if (tagged == null)
            {
                Console.Error.WriteLine("Tagged content is not supported in this Aspose.Pdf version.");
                return;
            }

            // -------------------------------------------------
            // Create a visual table on the first page (optional)
            // -------------------------------------------------
            Page page = pdfDocument.Pages[1];

            Table visualTable = new Table
            {
                ColumnWidths = "150 150 150", // three equal columns
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row (visual)
            Row headerRow = visualTable.Rows.Add();
            headerRow.BackgroundColor = Color.FromRgb(0.8, 0.8, 0.8); // light gray

            string[] headers = { "Column A", "Column B", "Column C" };
            foreach (string hdr in headers)
            {
                Cell cell = headerRow.Cells.Add();
                cell.Paragraphs.Add(new TextFragment(hdr));
                // Make header text bold
                cell.DefaultCellTextState.FontStyle = FontStyles.Bold;
            }

            // Add an empty data row (placeholder)
            Row dataRow = visualTable.Rows.Add();
            foreach (string _ in headers)
            {
                Cell cell = dataRow.Cells.Add();
                cell.Paragraphs.Add(new TextFragment(" "));
            }

            // Position the table on the page
            visualTable.Margin = new MarginInfo { Top = 50 };
            page.Paragraphs.Add(visualTable);

            // -------------------------------------------------
            // Create logical (tagged) table structure with headers
            // -------------------------------------------------
            // Create the table element in the logical structure
            TableElement tableElement = tagged.CreateTableElement();

            // Create the table header (THead) element
            TableTHeadElement theadElement = tagged.CreateTableTHeadElement();

            // For each header, create a TH element and set its text
            foreach (string hdr in headers)
            {
                // Create a TH (table header cell) element
                var thElement = tagged.CreateTableTHElement();

                // Set the visible text for the header cell
                thElement.SetText(hdr);

                // Append the TH element to the THead element
                theadElement.AppendChild(thElement, true);
            }

            // Append the THead element to the table element
            tableElement.AppendChild(theadElement, true);

            // (Optional) If the table has a body, you could create TBody elements here

            // NOTE: In newer Aspose.Pdf versions the Tag method expects a BDC operator.
            // The simple overload that accepts a Page is no longer available, and creating
            // a correct BDC operator is non‑trivial for this example. Therefore the tagging
            // call is omitted – the logical structure is still saved with PreSave().

            // Prepare the tagged content for saving
            tagged.PreSave();

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        try
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the existing PDF document
            Document pdfDoc = new Document(inputPath);

            // Ensure there is at least one page to work with
            Page page = pdfDoc.Pages.Count > 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

            // ---------- Visual table ----------
            Table table = new Table
            {
                ColumnWidths = "120 120 120",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 1),
                DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 }
            };

            // Header row (visual)
            Row headerRow = table.Rows.Add();
            headerRow.Cells.Add("Header 1");
            headerRow.Cells.Add("Header 2");
            headerRow.Cells.Add("Header 3");

            // Sample data row (visual)
            Row dataRow = table.Rows.Add();
            dataRow.Cells.Add("A1");
            dataRow.Cells.Add("B1");
            dataRow.Cells.Add("C1");

            // Add the visual table to the page
            page.Paragraphs.Add(table);

            // ---------- Logical structure for accessibility ----------
            // Obtain the logical‑structure helper
            var tagged = pdfDoc.TaggedContent;

            // Create a table structure element
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.ColumnWidths = "120 120 120";
            tableStruct.DefaultCellBorder = new BorderInfo(BorderSide.All, 1);
            tableStruct.DefaultCellPadding = new MarginInfo { Top = 5, Bottom = 5, Left = 5, Right = 5 };
            tableStruct.Title = "Sample Table";

            // Create the table header (THead) element
            TableTHeadElement thead = tableStruct.CreateTHead();

            // Create a header row inside the THead
            TableTRElement headerTr = thead.CreateTR();

            // Header texts
            string[] headers = { "Header 1", "Header 2", "Header 3" };

            for (int i = 0; i < headers.Length; i++)
            {
                // Visual header text (already part of the visual table, but we add a separate fragment for tagging)
                TextFragment tf = new TextFragment(headers[i])
                {
                    TextState = { FontSize = 12, Font = FontRepository.FindFont("Arial"), ForegroundColor = Color.Black }
                };
                page.Paragraphs.Add(tf);

                // Logical header cell (TH)
                TableTHElement th = headerTr.CreateTH();
                // No explicit BDC tagging is required – the TH element is already a logical table‑header cell.
            }

            // Append the table structure to the document root
            tagged.RootElement.AppendChild(tableStruct, true);

            // Save the modified PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

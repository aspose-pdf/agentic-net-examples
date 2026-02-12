using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged content interface
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set document language and title (optional but recommended for accessibility)
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with Tagged Table");

            // -------------------------------------------------
            // Create a logical table structure (tagged)
            // -------------------------------------------------
            // Create the root Table element
            TableElement tableElement = tagged.CreateTableElement();

            // Append the table to the document's root structure element
            tagged.RootElement.AppendChild(tableElement, true);

            // Create a Table Body (TBody) element and attach it to the table
            TableTBodyElement tBody = tagged.CreateTableTBodyElement();
            tableElement.AppendChild(tBody, true);

            // Create a single table row inside the body
            TableTRElement row = tBody.CreateTR();

            // Create a single table cell (TD) inside the row
            TableTDElement cell = row.CreateTD();

            // Create a paragraph element that will hold the actual text content
            ParagraphElement paragraph = tagged.CreateParagraphElement();

            // Append the paragraph to the cell
            cell.AppendChild(paragraph, true);

            // -------------------------------------------------
            // Add visual representation of the table on the first page
            // -------------------------------------------------
            Page page = pdfDocument.Pages[1];

            // Create a visual Table (Aspose.Pdf.Table) with one cell
            Table visualTable = new Table
            {
                ColumnWidths = "200", // single column width
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Add a row and a cell with the same text as the tagged paragraph
            Row visualRow = visualTable.Rows.Add();
            Cell visualCell = visualRow.Cells.Add();
            TextFragment tf = new TextFragment("Tagged table cell content");
            visualCell.Paragraphs.Add(tf);

            // Position the table on the page (example coordinates)
            visualTable.Margin = new MarginInfo { Top = 100, Left = 50 };
            page.Paragraphs.Add(visualTable);

            // -------------------------------------------------
            // Finalize tagged content and save the document
            // -------------------------------------------------
            tagged.PreSave();   // Prepare the logical structure for saving
            tagged.Save();      // Persist the tagged content into the PDF

            // Use the provided lifecycle rule for saving
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
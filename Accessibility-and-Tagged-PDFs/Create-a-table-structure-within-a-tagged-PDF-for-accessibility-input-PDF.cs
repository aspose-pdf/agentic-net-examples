using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output_tagged.pdf";

        try
        {
            // Verify source PDF exists
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // ------------------------------------------------------------
            // Add a simple visual table to the first page (optional visual aid)
            // ------------------------------------------------------------
            Page page = pdfDocument.Pages[1];
            Table table = new Table
            {
                ColumnWidths = "100 100 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f)
            };

            // Header row
            Row header = table.Rows.Add();
            header.Cells.Add(new TextFragment("Header 1"));
            header.Cells.Add(new TextFragment("Header 2"));
            header.Cells.Add(new TextFragment("Header 3"));

            // Data row
            Row data = table.Rows.Add();
            data.Cells.Add(new TextFragment("Cell 1"));
            data.Cells.Add(new TextFragment("Cell 2"));
            data.Cells.Add(new TextFragment("Cell 3"));

            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Create the logical (tagged) structure for the table
            // ------------------------------------------------------------
            ITaggedContent tagged = pdfDocument.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Tagged PDF with Table");

            // Create the root table element
            TableElement tableElement = tagged.CreateTableElement();
            tableElement.Title = "Sample Table";
            tableElement.AlternativeText = "A table with three columns and two rows";

            // Create table head and body elements
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();

            // Attach head and body to the table element
            tableElement.AppendChild(thead, true);
            tableElement.AppendChild(tbody, true);

            // ----- Table Header -----
            TableTRElement headerRow = thead.CreateTR(); // automatically added to thead
            TableTHElement th1 = headerRow.CreateTH();
            th1.Title = "Header 1";
            TableTHElement th2 = headerRow.CreateTH();
            th2.Title = "Header 2";
            TableTHElement th3 = headerRow.CreateTH();
            th3.Title = "Header 3";

            // ----- Table Body -----
            TableTRElement bodyRow = tbody.CreateTR(); // automatically added to tbody
            TableTDElement td1 = bodyRow.CreateTD();
            td1.Title = "Cell 1";
            TableTDElement td2 = bodyRow.CreateTD();
            td2.Title = "Cell 2";
            TableTDElement td3 = bodyRow.CreateTD();
            td3.Title = "Cell 3";

            // Prepare the tagged content before saving
            tagged.PreSave();

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
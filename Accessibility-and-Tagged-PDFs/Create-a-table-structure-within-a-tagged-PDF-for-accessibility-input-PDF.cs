using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_tagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Access the tagged‑content API
            ITaggedContent tagged = pdfDocument.TaggedContent;

            // Set document language and title for accessibility
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample PDF with Tagged Table");

            // -------------------------------------------------
            // 1. Add a visual table to the first page (optional)
            // -------------------------------------------------
            Page page = pdfDocument.Pages[1];

            Table visualTable = new Table
            {
                // Define three equal columns
                ColumnWidths = "100 100 100"
            };

            // Header row
            Row header = visualTable.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Two data rows
            for (int i = 0; i < 2; i++)
            {
                Row row = visualTable.Rows.Add();
                row.Cells.Add($"R{i + 1}C1");
                row.Cells.Add($"R{i + 1}C2");
                row.Cells.Add($"R{i + 1}C3");
            }

            // Add the visual table to the page content
            page.Paragraphs.Add(visualTable);

            // -------------------------------------------------
            // 2. Build the logical (tagged) structure for the table
            // -------------------------------------------------
            // Create the top‑level Table element
            TableElement tableElement = tagged.CreateTableElement();
            tableElement.BackgroundColor = Color.LightGray; // visual cue for the structure element

            // ----- Table Header (THead) -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            // CreateTR() already appends the TR to thead, so we do NOT call AppendChild again.
            TableTRElement trHead = thead.CreateTR();

            // Create three header cells (TH) and add them to the TR
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.Title = "Header 1";
            trHead.AppendChild(th1, true);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.Title = "Header 2";
            trHead.AppendChild(th2, true);

            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.Title = "Header 3";
            trHead.AppendChild(th3, true);

            // Thead already contains the TR, just add thead to the table
            tableElement.AppendChild(thead, true);

            // ----- Table Body (TBody) -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();

            // Two body rows, each with three cells (TD)
            for (int r = 0; r < 2; r++)
            {
                // CreateTR() automatically appends the TR to tbody.
                TableTRElement trBody = tbody.CreateTR();

                for (int c = 0; c < 3; c++)
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.Title = $"R{r + 1}C{c + 1}";
                    trBody.AppendChild(td, true);
                }
            }

            // Add tbody to the table
            tableElement.AppendChild(tbody, true);

            // Append the complete table structure to the document root
            tagged.RootElement.AppendChild(tableElement, true);

            // Prepare the tagged content before saving
            tagged.PreSave();

            // Save the modified PDF
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Tagged PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

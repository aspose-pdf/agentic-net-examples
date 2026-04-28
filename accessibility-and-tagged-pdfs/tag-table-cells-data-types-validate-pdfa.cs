using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF to tag
        const string outputPath = "tagged_output.pdf"; // result PDF
        const string logPath    = "validation_log.txt"; // validation log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle: load)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // 1. Create a visual table and add it to the first page
            // ------------------------------------------------------------
            Page page = doc.Pages[1];

            Table visualTable = new Table
            {
                ColumnWidths = "100 150", // two columns
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // Header row
            Row headerRow = visualTable.Rows.Add();
            headerRow.Cells.Add("ID");
            headerRow.Cells.Add("Value");

            // Data rows
            Row row1 = visualTable.Rows.Add();
            row1.Cells.Add("1");
            row1.Cells.Add("42"); // numeric

            Row row2 = visualTable.Rows.Add();
            row2.Cells.Add("2");
            row2.Cells.Add("Sample text"); // string

            page.Paragraphs.Add(visualTable);

            // ------------------------------------------------------------
            // 2. Build the corresponding logical‑structure table
            // ------------------------------------------------------------
            TableElement structTable = tagged.CreateTableElement();
            structTable.AlternativeText = "Sample data table";
            root.AppendChild(structTable); // AppendChild with one argument

            // Create table header (THead)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            structTable.AppendChild(thead);

            TableTRElement headerStructRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerStructRow);

            // Header cells (TH)
            TableTHElement thId = tagged.CreateTableTHElement();
            thId.SetText("ID");
            thId.SetTag("DataType:Header");
            headerStructRow.AppendChild(thId);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            thValue.SetTag("DataType:Header");
            headerStructRow.AppendChild(thValue);

            // Create table body (TBody)
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            structTable.AppendChild(tbody);

            // First data row
            TableTRElement bodyRow1 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow1);

            TableTDElement tdId1 = tagged.CreateTableTDElement();
            tdId1.SetText("1");
            tdId1.SetTag("DataType:String"); // IDs are strings
            bodyRow1.AppendChild(tdId1);

            TableTDElement tdValue1 = tagged.CreateTableTDElement();
            tdValue1.SetText("42");
            tdValue1.SetTag("DataType:Number");
            bodyRow1.AppendChild(tdValue1);

            // Second data row
            TableTRElement bodyRow2 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow2);

            TableTDElement tdId2 = tagged.CreateTableTDElement();
            tdId2.SetText("2");
            tdId2.SetTag("DataType:String");
            bodyRow2.AppendChild(tdId2);

            TableTDElement tdValue2 = tagged.CreateTableTDElement();
            tdValue2.SetText("Sample text");
            tdValue2.SetTag("DataType:String");
            bodyRow2.AppendChild(tdValue2);

            // ------------------------------------------------------------
            // 3. Validate the PDF for PDF/A‑1B compliance (or any format)
            // ------------------------------------------------------------
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log: {logPath})");

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "tagged_output.pdf"; // PDF with custom tags
        const string logPath    = "validation_log.txt"; // validation report

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // ------------------------------------------------------------
            // Create a logical table element and attach it to the root
            // ------------------------------------------------------------
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with custom cell tags";
            root.AppendChild(table); // AppendChild with one argument

            // ------------------------------------------------------------
            // Header row (TH cells)
            // ------------------------------------------------------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Example column headers
            string[] headers = { "ID", "Name", "BirthDate", "Salary" };
            foreach (string hdr in headers)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(hdr);
                // No custom tag needed for header cells
                headerRow.AppendChild(th);
            }

            // ------------------------------------------------------------
            // Body rows (TD cells) – assign custom tags indicating data type
            // ------------------------------------------------------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Example data row
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // Cell 1 – ID (integer)
            TableTDElement tdId = tagged.CreateTableTDElement();
            tdId.SetText("1001");
            tdId.SetTag("Number"); // custom tag
            dataRow.AppendChild(tdId);

            // Cell 2 – Name (string)
            TableTDElement tdName = tagged.CreateTableTDElement();
            tdName.SetText("Alice Smith");
            tdName.SetTag("String");
            dataRow.AppendChild(tdName);

            // Cell 3 – BirthDate (date)
            TableTDElement tdDate = tagged.CreateTableTDElement();
            tdDate.SetText("1985-04-23");
            tdDate.SetTag("Date");
            dataRow.AppendChild(tdDate);

            // Cell 4 – Salary (currency)
            TableTDElement tdSalary = tagged.CreateTableTDElement();
            tdSalary.SetText("$75,000");
            tdSalary.SetTag("Currency");
            dataRow.AppendChild(tdSalary);

            // ------------------------------------------------------------
            // OPTIONAL: add a visual representation of the table to the first page
            // ------------------------------------------------------------
            Page firstPage = doc.Pages[1];
            Table visualTable = new Table
            {
                ColumnWidths = "100 150 120 100",
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };
            // Header row
            Row vHeader = visualTable.Rows.Add();
            foreach (string hdr in headers)
                vHeader.Cells.Add(hdr);
            // Data row
            Row vData = visualTable.Rows.Add();
            vData.Cells.Add("1001");
            vData.Cells.Add("Alice Smith");
            vData.Cells.Add("1985-04-23");
            vData.Cells.Add("$75,000");

            firstPage.Paragraphs.Add(visualTable);

            // ------------------------------------------------------------
            // Validate the document (PDF/A‑1B compliance as an example)
            // ------------------------------------------------------------
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log: {logPath})");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
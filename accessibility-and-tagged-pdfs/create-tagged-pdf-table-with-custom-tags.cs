using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "TaggedTable.pdf";
        const string validationLog = "validation_log.txt";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Visual representation of the table (optional)
            // -------------------------------------------------
            // The visual table uses GDI+ internally. Guard its creation on non‑Windows platforms.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Table visualTable = new Table
                {
                    ColumnWidths = "100 100",
                    DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
                };

                // Header row
                Row headerRow = visualTable.Rows.Add();
                headerRow.Cells.Add("ID");
                headerRow.Cells.Add("Value");

                // First data row
                Row dataRow1 = visualTable.Rows.Add();
                dataRow1.Cells.Add("1");
                dataRow1.Cells.Add("100");

                // Second data row
                Row dataRow2 = visualTable.Rows.Add();
                dataRow2.Cells.Add("2");
                dataRow2.Cells.Add("200");

                // Add the visual table to the page
                page.Paragraphs.Add(visualTable);
            }
            else
            {
                Console.WriteLine("Skipping visual table creation – GDI+ (libgdiplus) is not available on this platform.");
            }

            // -------------------------------------------------
            // Logical structure (tagged PDF) with custom tags
            // -------------------------------------------------
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Sample Tagged Table");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a Table structure element
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.AlternativeText = "Sample data table";
            root.AppendChild(tableStruct);

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            tableStruct.AppendChild(thead);

            TableTRElement headerStructRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerStructRow);

            TableTHElement thId = tagged.CreateTableTHElement();
            thId.SetText("ID");
            thId.SetTag("Header");
            headerStructRow.AppendChild(thId);

            TableTHElement thValue = tagged.CreateTableTHElement();
            thValue.SetText("Value");
            thValue.SetTag("Header");
            headerStructRow.AppendChild(thValue);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            tableStruct.AppendChild(tbody);

            // First data row
            TableTRElement bodyRow1 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow1);

            TableTDElement tdId1 = tagged.CreateTableTDElement();
            tdId1.SetText("1");
            tdId1.SetTag("Number"); // Custom tag indicating data type
            bodyRow1.AppendChild(tdId1);

            TableTDElement tdValue1 = tagged.CreateTableTDElement();
            tdValue1.SetText("100");
            tdValue1.SetTag("Currency"); // Custom tag indicating data type
            bodyRow1.AppendChild(tdValue1);

            // Second data row
            TableTRElement bodyRow2 = tagged.CreateTableTRElement();
            tbody.AppendChild(bodyRow2);

            TableTDElement tdId2 = tagged.CreateTableTDElement();
            tdId2.SetText("2");
            tdId2.SetTag("Number");
            bodyRow2.AppendChild(tdId2);

            TableTDElement tdValue2 = tagged.CreateTableTDElement();
            tdValue2.SetText("200");
            tdValue2.SetTag("Currency");
            bodyRow2.AppendChild(tdValue2);

            // -------------------------------------------------
            // Save the PDF (guarded for platforms without libgdiplus)
            // -------------------------------------------------
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdf);
                Console.WriteLine($"PDF saved to '{outputPdf}'.");
            }
            else
            {
                Console.WriteLine("Skipping doc.Save() because GDI+ (libgdiplus) is not available on this platform.");
            }

            // -------------------------------------------------
            // Validate the PDF against PDF/A-1B (example format)
            // -------------------------------------------------
            bool isValid = false;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
                Console.WriteLine(isValid
                    ? $"PDF validation succeeded. Log written to '{validationLog}'."
                    : $"PDF validation failed. See log at '{validationLog}'.");
            }
            else
            {
                Console.WriteLine("PDF validation skipped on non‑Windows platform (requires GDI+).");
            }
        }
    }
}

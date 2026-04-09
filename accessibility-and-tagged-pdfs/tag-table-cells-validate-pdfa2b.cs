using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Validation; // added for ValidationResult

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "tagged_output.pdf"; // result PDF
        const string logPath    = "validation_log.txt"; // validation log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with custom‑tagged table cells");

            // Root of the logical structure tree
            StructureElement root = tagged.RootElement;

            // -----------------------------------------------------------------
            // Build a logical table structure and assign custom tags to cells
            // -----------------------------------------------------------------
            // Create the table element and attach it to the root
            TableElement tableStruct = tagged.CreateTableElement();
            tableStruct.AlternativeText = "Sample data table";
            root.AppendChild(tableStruct); // AppendChild with one argument

            // Sample data (first row = header)
            string[,] data = new string[,] {
                { "Name", "Age", "Salary" },
                { "Alice", "30", "55000" },
                { "Bob",   "45", "72000" }
            };

            // ----- Header row ------------------------------------------------
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            tableStruct.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            for (int col = 0; col < data.GetLength(1); col++)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(data[0, col]);   // header text
                th.SetTag("header");        // custom tag for header cells
                headerRow.AppendChild(th);
            }

            // ----- Body rows -------------------------------------------------
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            tableStruct.AppendChild(tbody);

            for (int row = 1; row < data.GetLength(0); row++)
            {
                TableTRElement bodyRow = tagged.CreateTableTRElement();
                tbody.AppendChild(bodyRow);

                for (int col = 0; col < data.GetLength(1); col++)
                {
                    TableTDElement td = tagged.CreateTableTDElement();
                    td.SetText(data[row, col]);

                    // Determine data type and assign a custom tag
                    if (int.TryParse(data[row, col], out _) ||
                        double.TryParse(data[row, col], out _))
                    {
                        td.SetTag("numeric");
                    }
                    else
                    {
                        td.SetTag("text");
                    }

                    bodyRow.AppendChild(td);
                }
            }

            // -----------------------------------------------------------------
            // Save the modified PDF (lifecycle rule: use Document.Save)
            // -----------------------------------------------------------------
            doc.Save(outputPath);

            // -----------------------------------------------------------------
            // Validate the PDF (e.g., PDF/A‑2B compliance) and write log
            // -----------------------------------------------------------------
            ValidationResult validationResult = doc.Validate(logPath, PdfFormat.PDF_A_2B);
            bool isValid = validationResult.IsValid;
            Console.WriteLine($"Validation result: {isValid}");
            Console.WriteLine($"Validation log written to: {logPath}");
        }
    }
}

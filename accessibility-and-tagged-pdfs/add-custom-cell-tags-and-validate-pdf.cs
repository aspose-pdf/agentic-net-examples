using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tagged_output.pdf";
        const string logPath    = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Access tagged‑content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("PDF with custom cell tags");

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with custom tags";
            root.AppendChild(table); // attach table to the root

            // ----- Create table header row -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Header cells (TH)
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("ID");
            headerRow.AppendChild(th1);

            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Amount");
            headerRow.AppendChild(th2);

            // ----- Create table body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);

            TableTDElement td1 = tagged.CreateTableTDElement();
            td1.SetText("001");
            td1.SetTag("Identifier"); // custom tag for data type
            row1.AppendChild(td1);

            TableTDElement td2 = tagged.CreateTableTDElement();
            td2.SetText("1234.56");
            td2.SetTag("Number"); // custom tag for numeric data
            row1.AppendChild(td2);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);

            TableTDElement td3 = tagged.CreateTableTDElement();
            td3.SetText("002");
            td3.SetTag("Identifier");
            row2.AppendChild(td3);

            TableTDElement td4 = tagged.CreateTableTDElement();
            td4.SetText("7890.12");
            td4.SetTag("Number");
            row2.AppendChild(td4);

            // Save the modified PDF
            doc.Save(outputPath);

            // Validate the PDF (e.g., against PDF/A‑1B)
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine(isValid
                ? $"PDF is compliant. Validation log saved to '{logPath}'."
                : $"PDF is NOT compliant. See log at '{logPath}'.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";
        const string validationLogPath = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Access tagged content (creates structure if not present)
                ITaggedContent taggedContent = doc.TaggedContent;
                taggedContent.SetLanguage("en-US");
                taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

                // Root element of the structure tree
                StructureElement root = taggedContent.RootElement;

                // Create a table element
                TableElement table = taggedContent.CreateTableElement();
                table.AlternativeText = "Sample data table with custom data‑type tags";
                root.AppendChild(table);

                // Create table header
                TableTHeadElement thead = taggedContent.CreateTableTHeadElement();
                table.AppendChild(thead);
                TableTRElement headerRow = taggedContent.CreateTableTRElement();
                thead.AppendChild(headerRow);

                // Header cells
                TableTHElement thName = taggedContent.CreateTableTHElement();
                thName.SetText("Name");
                thName.AlternativeText = "String"; // custom tag indicating data type
                headerRow.AppendChild(thName);

                TableTHElement thAmount = taggedContent.CreateTableTHElement();
                thAmount.SetText("Amount");
                thAmount.AlternativeText = "Currency";
                headerRow.AppendChild(thAmount);

                TableTHElement thDate = taggedContent.CreateTableTHElement();
                thDate.SetText("Date");
                thDate.AlternativeText = "Date";
                headerRow.AppendChild(thDate);

                // Create table body
                TableTBodyElement tbody = taggedContent.CreateTableTBodyElement();
                table.AppendChild(tbody);

                // Example data rows
                AddDataRow(taggedContent, tbody, "Widget A", "123.45", "2023-01-15");
                AddDataRow(taggedContent, tbody, "Widget B", "678.90", "2023-02-20");

                // Save the modified PDF
                doc.Save(outputPath);

                // Validate the resulting PDF for PDF/UA compliance
                // The Validate method returns a bool indicating compliance and writes a log file.
                bool isValid = doc.Validate(validationLogPath, PdfFormat.PDF_UA_1);
                Console.WriteLine($"Validation result: {(isValid ? "Valid" : "Invalid")}");
                if (!isValid)
                {
                    Console.WriteLine($"See validation log for details: {validationLogPath}");
                }
                else
                {
                    Console.WriteLine("PDF complies with PDF/UA 1.0 profile.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper to add a data row with custom data‑type tags on each cell
    static void AddDataRow(ITaggedContent tagged, TableTBodyElement tbody,
                           string name, string amount, string date)
    {
        TableTRElement row = tagged.CreateTableTRElement();
        tbody.AppendChild(row);

        TableTDElement tdName = tagged.CreateTableTDElement();
        tdName.SetText(name);
        tdName.AlternativeText = "String";
        row.AppendChild(tdName);

        TableTDElement tdAmount = tagged.CreateTableTDElement();
        tdAmount.SetText(amount);
        tdAmount.AlternativeText = "Currency";
        row.AppendChild(tdAmount);

        TableTDElement tdDate = tagged.CreateTableTDElement();
        tdDate.SetText(date);
        tdDate.AlternativeText = "Date";
        row.AppendChild(tdDate);
    }
}

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
        const string logPath = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Enable tagged content and set basic properties
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the logical structure tree
            StructureElement root = tagged.RootElement;

            // Create a table structure element
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table with custom tags";
            root.AppendChild(table);

            // Header row (optional)
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement th1 = tagged.CreateTableTHElement();
            th1.SetText("ID");
            headerRow.AppendChild(th1);
            TableTHElement th2 = tagged.CreateTableTHElement();
            th2.SetText("Name");
            headerRow.AppendChild(th2);
            TableTHElement th3 = tagged.CreateTableTHElement();
            th3.SetText("Amount");
            headerRow.AppendChild(th3);

            // Body of the table
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // First data row
            TableTRElement row1 = tagged.CreateTableTRElement();
            tbody.AppendChild(row1);
            TableTDElement td11 = tagged.CreateTableTDElement();
            td11.SetText("1");
            td11.SetTag("Number"); // custom tag indicating data type
            row1.AppendChild(td11);
            TableTDElement td12 = tagged.CreateTableTDElement();
            td12.SetText("Alice");
            td12.SetTag("String");
            row1.AppendChild(td12);
            TableTDElement td13 = tagged.CreateTableTDElement();
            td13.SetText("123.45");
            td13.SetTag("Currency");
            row1.AppendChild(td13);

            // Second data row
            TableTRElement row2 = tagged.CreateTableTRElement();
            tbody.AppendChild(row2);
            TableTDElement td21 = tagged.CreateTableTDElement();
            td21.SetText("2");
            td21.SetTag("Number");
            row2.AppendChild(td21);
            TableTDElement td22 = tagged.CreateTableTDElement();
            td22.SetText("Bob");
            td22.SetTag("String");
            row2.AppendChild(td22);
            TableTDElement td23 = tagged.CreateTableTDElement();
            td23.SetText("987.00");
            td23.SetTag("Currency");
            row2.AppendChild(td23);

            // Save the modified PDF
            doc.Save(outputPath);

            // Validate the saved PDF (example: PDF/A-1B compliance)
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine(isValid
                ? $"Validation succeeded. Log written to {logPath}"
                : $"Validation failed. See log at {logPath}");
        }
    }
}
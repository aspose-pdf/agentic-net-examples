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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content API
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with custom tags");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // Create table header
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);
            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            headerRow.AppendChild(thName);
            TableTHElement thAge = tagged.CreateTableTHElement();
            thAge.SetText("Age");
            headerRow.AppendChild(thAge);

            // Create table body with one data row
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);
            TableTRElement dataRow = tagged.CreateTableTRElement();
            tbody.AppendChild(dataRow);

            // First cell – string data type
            TableTDElement tdName = tagged.CreateTableTDElement();
            tdName.SetText("Alice");
            tdName.SetTag("DataType:String");
            dataRow.AppendChild(tdName);

            // Second cell – integer data type
            TableTDElement tdAge = tagged.CreateTableTDElement();
            tdAge.SetText("30");
            tdAge.SetTag("DataType:Integer");
            dataRow.AppendChild(tdAge);

            // Save the modified PDF
            doc.Save(outputPath);

            // Validate the PDF for PDF/A-1B compliance
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine(isValid ? "PDF is compliant." : "PDF has compliance issues. See log.");
        }
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;
using Aspose.Pdf.Facades; // Facade namespace included as requested

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

        // Load the source PDF and create tagged content
        using (Document doc = new Document(inputPath))
        {
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("Table with custom tags");

            // Root element of the logical structure
            StructureElement root = tagged.RootElement;

            // Create a table element and attach it to the root
            TableElement table = tagged.CreateTableElement();
            table.AlternativeText = "Sample data table";
            root.AppendChild(table);

            // ----- Table Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            table.AppendChild(thead);
            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            TableTHElement thId = tagged.CreateTableTHElement();
            thId.SetText("ID");
            thId.SetTag("DataType:Integer");
            headerRow.AppendChild(thId);

            TableTHElement thName = tagged.CreateTableTHElement();
            thName.SetText("Name");
            thName.SetTag("DataType:String");
            headerRow.AppendChild(thName);

            TableTHElement thPrice = tagged.CreateTableTHElement();
            thPrice.SetText("Price");
            thPrice.SetTag("DataType:Decimal");
            headerRow.AppendChild(thPrice);

            // ----- Table Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            table.AppendChild(tbody);

            // Add a few data rows with custom tags on each cell
            for (int i = 1; i <= 3; i++)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement tdId = tagged.CreateTableTDElement();
                tdId.SetText(i.ToString());
                tdId.SetTag("DataType:Integer");
                dataRow.AppendChild(tdId);

                TableTDElement tdName = tagged.CreateTableTDElement();
                tdName.SetText($"Item {i}");
                tdName.SetTag("DataType:String");
                dataRow.AppendChild(tdName);

                TableTDElement tdPrice = tagged.CreateTableTDElement();
                tdPrice.SetText((i * 9.99).ToString("F2"));
                tdPrice.SetTag("DataType:Decimal");
                dataRow.AppendChild(tdPrice);
            }

            // Save the tagged PDF
            doc.Save(outputPath);
        }

        // Validate the resulting PDF for PDF/UA compliance
        using (Document validatedDoc = new Document(outputPath))
        {
            bool isCompliant = validatedDoc.Validate(logPath, PdfFormat.PDF_UA_1);
            Console.WriteLine($"PDF/UA compliance: {isCompliant}");
            Console.WriteLine($"Validation log written to: {logPath}");
        }
    }
}
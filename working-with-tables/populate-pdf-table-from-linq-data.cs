using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    // Simple data model
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    static void Main()
    {
        // Sample data source
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice",   Age = 30, City = "New York" },
            new Person { Name = "Bob",     Age = 22, City = "London"   },
            new Person { Name = "Charlie", Age = 19, City = "Paris"    },
            new Person { Name = "Diana",   Age = 27, City = "Tokyo"    }
        };

        // LINQ query – filter and project
        var query = people
            .Where(p => p.Age > 20)
            .Select(p => new { p.Name, p.Age, p.City });

        // Convert query result to a DataTable
        DataTable tableData = new DataTable();
        // Add columns based on the anonymous type's properties
        tableData.Columns.Add("Name", typeof(string));
        tableData.Columns.Add("Age",  typeof(int));
        tableData.Columns.Add("City", typeof(string));

        // Populate rows
        foreach (var item in query)
        {
            DataRow row = tableData.NewRow();
            row["Name"] = item.Name;
            row["Age"]  = item.Age;
            row["City"] = item.City;
            tableData.Rows.Add(row);
        }

        // Create PDF and add a tagged table populated from the DataTable
        const string outputPath = "TableFromLinq.pdf";

        using (Document doc = new Document())
        {
            // Enable tagging and set language/title
            ITaggedContent tagged = doc.TaggedContent;
            tagged.SetLanguage("en-US");
            tagged.SetTitle("LINQ Query Result Table");

            // Root element of the structure tree
            StructureElement root = tagged.RootElement;

            // Create the table element
            TableElement pdfTable = tagged.CreateTableElement();
            pdfTable.AlternativeText = "Table showing filtered persons from LINQ query.";
            root.AppendChild(pdfTable);

            // ----- Header -----
            TableTHeadElement thead = tagged.CreateTableTHeadElement();
            pdfTable.AppendChild(thead);

            TableTRElement headerRow = tagged.CreateTableTRElement();
            thead.AppendChild(headerRow);

            // Helper to create a header cell
            TableTHElement CreateHeader(string text)
            {
                TableTHElement th = tagged.CreateTableTHElement();
                th.SetText(text);
                return th;
            }

            headerRow.AppendChild(CreateHeader("Name"));
            headerRow.AppendChild(CreateHeader("Age"));
            headerRow.AppendChild(CreateHeader("City"));

            // ----- Body -----
            TableTBodyElement tbody = tagged.CreateTableTBodyElement();
            pdfTable.AppendChild(tbody);

            foreach (DataRow dr in tableData.Rows)
            {
                TableTRElement dataRow = tagged.CreateTableTRElement();
                tbody.AppendChild(dataRow);

                TableTDElement tdName = tagged.CreateTableTDElement();
                tdName.SetText(dr["Name"].ToString());
                dataRow.AppendChild(tdName);

                TableTDElement tdAge = tagged.CreateTableTDElement();
                tdAge.SetText(dr["Age"].ToString());
                dataRow.AppendChild(tdAge);

                TableTDElement tdCity = tagged.CreateTableTDElement();
                tdCity.SetText(dr["City"].ToString());
                dataRow.AppendChild(tdCity);
            }

            // Save the PDF (no PreSave needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}
using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample source PDF (required by the self‑contained rule)
        using (Document sourceDoc = new Document())
        {
            sourceDoc.Pages.Add();
            sourceDoc.Save("input.pdf");
        }

        // Step 2: Build a sample DataTable (max 4 rows for evaluation mode)
        DataTable table = new DataTable("Sample");
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Name", typeof(string));
        for (int i = 1; i <= 3; i++)
        {
            DataRow row = table.NewRow();
            row["Id"] = i;
            row["Name"] = "Row " + i;
            table.Rows.Add(row);
        }

        // Step 3: Load the sample PDF and add a page for each DataTable row
        using (Document doc = new Document("input.pdf"))
        {
            foreach (DataRow dataRow in table.Rows)
            {
                Page page = doc.Pages.Add();
                TextFragment tf = new TextFragment("Id: " + dataRow["Id"] + ", Name: " + dataRow["Name"]);
                tf.Position = new Aspose.Pdf.Text.Position(100, 700);
                page.Paragraphs.Add(tf);
            }

            doc.Save("output.pdf");
        }

        // Step 4: Verify that the resulting PDF has one page per DataTable row
        using (Document resultDoc = new Document("output.pdf"))
        {
            int expectedPages = table.Rows.Count;
            int actualPages = resultDoc.Pages.Count;
            Console.WriteLine("Expected pages: " + expectedPages);
            Console.WriteLine("Actual pages: " + actualPages);
            Console.WriteLine("Verification: " + (expectedPages == actualPages ? "PASS" : "FAIL"));
        }
    }
}

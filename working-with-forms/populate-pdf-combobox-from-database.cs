using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // ----- 1. Retrieve combo box items from a database (mocked here) -----
        List<string> comboItems = GetComboBoxItemsFromDatabase();

        // ----- 2. Open (or create) the PDF document -----
        const string inputPdfPath  = "template.pdf";   // existing PDF with a page to place the field
        const string outputPdfPath = "filled.pdf";

        using (Document pdfDoc = new Document())
        {
            // If you have an existing PDF, load it instead:
            // using (Document pdfDoc = new Document(inputPdfPath))
            // {
            //     // continue with the same logic
            // }

            // Ensure at least one page exists
            Page page = pdfDoc.Pages.Count > 0 ? pdfDoc.Pages[1] : pdfDoc.Pages.Add();

            // ----- 3. Create the ComboBox field -----
            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);
            ComboBoxField comboBox = new ComboBoxField(page, rect)
            {
                Name = "MyComboBox",          // field name (used for later reference)
                PartialName = "MyComboBox",
                Editable = true               // allow user to type a value
            };

            // ----- 4. Populate the combo box with items -----
            foreach (string item in comboItems)
            {
                // AddOption(string) adds an item with the same display and export value
                comboBox.AddOption(item);
            }

            // ----- 5. Add the field to the document's form -----
            pdfDoc.Form.Add(comboBox);

            // ----- 6. Save the updated PDF -----
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
    }

    // Mocked database call – replace with real ADO.NET code as needed
    static List<string> GetComboBoxItemsFromDatabase()
    {
        // Example using ADO.NET (uncomment and adjust connection string / query):
        /*
        var items = new List<string>();
        string connectionString = "Data Source=SERVER;Initial Catalog=DB;Integrated Security=True;";
        string query = "SELECT Name FROM ComboItems ORDER BY Name";

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    items.Add(reader.GetString(0));
                }
            }
        }
        return items;
        */

        // For demonstration purposes, return a static list
        return new List<string> { "Option A", "Option B", "Option C" };
    }
}
using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the combo box will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the combo box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create the combo box field on the selected page
            ComboBoxField combo = new ComboBoxField(page, rect)
            {
                Name = "MyCombo",
                PartialName = "MyCombo",
                Editable = true,
                // Set default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black)
            };

            // Add the combo box to the page's annotation collection
            page.Annotations.Add(combo);

            // -----------------------------------------------------------------
            // Instead of a real database connection (which would require the
            // System.Data.SqlClient package), create an in‑memory DataTable that
            // mimics the data that would be read from the database.
            // -----------------------------------------------------------------
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Rows.Add("Apple");
            dt.Rows.Add("Banana");
            dt.Rows.Add("Cherry");
            dt.Rows.Add("Date");
            dt.Rows.Add("Elderberry");

            // Populate the combo box with the items from the DataTable
            foreach (DataRow row in dt.Rows)
            {
                string item = row["ItemName"].ToString();
                combo.AddOption(item);
            }

            // Optionally select the first item (indices are 1‑based)
            if (combo.Options.Count > 0)
                combo.Selected = 1;

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Combo box populated and saved to '{outputPdf}'.");
    }
}

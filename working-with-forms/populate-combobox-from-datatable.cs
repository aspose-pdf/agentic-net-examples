using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Simulate database retrieval with an in‑memory DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("Option", typeof(string));
        dt.Rows.Add("Apple");
        dt.Rows.Add("Banana");
        dt.Rows.Add("Cherry");

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the combo box (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Instantiate the combo box field on the page
            ComboBoxField comboBox = new ComboBoxField(page, comboRect);
            comboBox.PartialName = "FruitCombo";   // field name
            comboBox.Editable = true;              // allow user to type a value

            // Add each option retrieved from the DataTable to the combo box
            foreach (DataRow row in dt.Rows)
            {
                string option = row["Option"].ToString();
                comboBox.AddOption(option);
            }

            // Register the field with the document's form
            doc.Form.Add(comboBox);

            // Save the PDF to disk
            doc.Save("ComboBoxDemo.pdf");
        }

        Console.WriteLine("PDF with populated combo box created successfully.");
    }
}
using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PopulateComboBoxExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a sample PDF file
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save("input.pdf");
            }

            // Step 2: Open the PDF and add a ComboBox field
            using (Document pdfDoc = new Document("input.pdf"))
            {
                // Define the rectangle where the combo box will appear
                Rectangle rect = new Rectangle(100, 600, 200, 620);

                // Create the combo box field on the first page
                ComboBoxField comboBox = new ComboBoxField(pdfDoc, rect);
                comboBox.PartialName = "Country";
                comboBox.Editable = true;

                // Simulate retrieving values from a database (max 4 items for evaluation mode)
                string[] dbValues = new string[] { "USA", "Canada", "Mexico", "Germany" };
                foreach (string value in dbValues)
                {
                    comboBox.AddOption(value);
                }

                // Add the field to the form on page 1 (1‑based indexing)
                pdfDoc.Form.Add(comboBox, 1);

                // Save the updated PDF
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
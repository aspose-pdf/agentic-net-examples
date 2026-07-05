using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "DropdownForm.pdf";

        // Sample items for the dropdown list
        string[] items = { "Option A", "Option B", "Option C", "Option D" };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the combo box will be placed
            // (llx, lly, urx, ury) – lower‑left and upper‑right coordinates
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the combo box field on the page
            ComboBoxField comboBox = new ComboBoxField(page, rect)
            {
                // Set a name for the field (used to identify it later)
                PartialName = "SampleComboBox",
                // Optional: set a visible border color
                Color = Color.Black,
                // Optional: set the default selected index (1‑based). 0 means no selection.
                Selected = 0
            };

            // Populate the combo box with options from the array
            foreach (string item in items)
            {
                comboBox.AddOption(item);
            }

            // Add the combo box to the document's form collection (NOT to the page)
            doc.Form.Add(comboBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown list saved to '{outputPath}'.");
    }
}

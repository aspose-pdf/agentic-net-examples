using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_dropdown.pdf";

        // Options to populate the dropdown
        string[] dropdownItems = { "Option 1", "Option 2", "Option 3", "Option 4" };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the combo box will appear
            // Aspose.Pdf.Rectangle expects lower‑left (x1,y1) and upper‑right (x2,y2) coordinates.
            // Here we place the combo box at (100,500) with a width of 200 and a height of 20.
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the combo box field on the page
            ComboBoxField comboBox = new ComboBoxField(page, comboRect)
            {
                PartialName = "SampleComboBox", // <-- note the trailing comma
                // Selected = 1; // optional: set default selected index (1‑based) after adding options
            };

            // Populate the combo box with items from the array
            foreach (string item in dropdownItems)
            {
                comboBox.AddOption(item);
            }

            // Optionally set the default selected item (first item)
            if (dropdownItems.Length > 0)
                comboBox.Selected = 1; // 1‑based index

            // Add the field to the document's form (constructor already registers it, this is safe)
            doc.Form.Add(comboBox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}

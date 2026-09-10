using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "DropdownForm.pdf";

        // Sample items for the dropdown list
        string[] dropdownItems = { "Option A", "Option B", "Option C", "Option D" };

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown will appear (llx, lly, urx, ury)
            var rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a ComboBox (dropdown) field on the page
            ComboBoxField comboBox = new ComboBoxField(page, rect)
            {
                // Set a unique name for the field
                PartialName = "SampleDropdown",
                // Optional: set a default selected index (1‑based). 0 means no selection.
                Selected = 0
            };

            // Populate the dropdown list with items from the array using AddOption
            foreach (string item in dropdownItems)
            {
                comboBox.AddOption(item);
            }

            // Add the field to the document's form collection (optional but explicit)
            doc.Form.Add(comboBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown list saved to '{outputPath}'.");
    }
}

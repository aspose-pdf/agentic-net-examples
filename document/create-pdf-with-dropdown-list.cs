using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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
            // Add a blank page (required for placing the field)
            Page page = doc.Pages.Add();

            // Define the rectangle where the ComboBox (dropdown) will appear
            // Rectangle(left, bottom, right, top)
            // Here we want a box 200 points wide and 20 points high starting at (100,600)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a ComboBox field on the specific page within the defined rectangle
            ComboBoxField combo = new ComboBoxField(page, rect)
            {
                // Set a unique name for the field (used when accessing the field later)
                PartialName = "SampleDropdown"
            };

            // Populate the dropdown with items from the array
            foreach (string item in items)
            {
                combo.AddOption(item);
            }

            // Optionally set a default selected value (index starts at 1)
            combo.Selected = 1; // selects "Option A"

            // Add the field to the form (required for the field to be part of the PDF form)
            doc.Form.Add(combo);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown list saved to '{outputPath}'.");
    }
}

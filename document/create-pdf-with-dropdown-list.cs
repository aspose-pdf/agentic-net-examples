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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown will appear
            // Parameters: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a ComboBox (dropdown) field on the page
            ComboBoxField combo = new ComboBoxField(page, rect)
            {
                // Set a unique name for the field
                PartialName = "SampleDropdown",
                // Optional: set a tooltip (alternate name)
                AlternateName = "Select an option"
            };

            // Populate the dropdown with the items from the array
            foreach (string item in items)
            {
                combo.AddOption(item);
            }

            // Add the field to the document's form collection (optional – the constructor already registers it)
            doc.Form.Add(combo);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}
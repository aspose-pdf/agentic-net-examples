using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "dropdown_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the combo box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a combo box (dropdown) field on the page
            ComboBoxField combo = new ComboBoxField(page, rect)
            {
                Name = "SampleCombo",
                PartialName = "SampleCombo",
                Color = Aspose.Pdf.Color.LightGray // optional visual styling
            };

            // Populate the dropdown list from an array
            string[] items = { "Apple", "Banana", "Cherry", "Date" };
            foreach (string item in items)
            {
                combo.AddOption(item);
            }

            // Set the default selected item (1‑based index; 0 means no selection)
            combo.Selected = 1; // selects "Apple"

            // Add the field to the document's form collection
            doc.Form.Add(combo);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "form_dropdown.pdf";
        string[] options = { "Option A", "Option B", "Option C", "Option D" };

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle for the dropdown (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a ComboBox (dropdown) form field
            ComboBoxField combo = new ComboBoxField(page, comboRect);
            combo.PartialName = "SampleCombo";
            // Tooltip is set via AlternateName
            combo.AlternateName = "Select an option";

            // Populate the dropdown list from the array using AddOption
            foreach (string opt in options)
            {
                combo.AddOption(opt);
            }

            // Set the default selected item (1‑based index)
            combo.Selected = 1; // selects the first option

            // Add the field to the document's form collection (page index is 1‑based)
            doc.Form.Add(combo, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dropdown saved to '{outputPath}'.");
    }
}

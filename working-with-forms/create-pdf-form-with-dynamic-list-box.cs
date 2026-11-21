using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicListForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is at index 1)
            Page page = doc.Pages.Add();

            // Define the rectangle for the combo box field (llx, lly, urx, ury)
            // Use Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a ComboBoxField (editable list) on the page
            ComboBoxField combo = new ComboBoxField(page, comboRect)
            {
                PartialName = "DynamicList",   // field name
                Editable    = true,           // allows users to add new items
                Value       = "Select or type..."
            };

            // Add some initial items to the list using AddOption (the correct API)
            combo.AddOption("Item 1");
            combo.AddOption("Item 2");
            combo.AddOption("Item 3");

            // Add the combo box to the document's form collection
            doc.Form.Add(combo);

            // Optional: add a label for the field
            TextFragment label = new TextFragment("Dynamic List:");
            label.Position = new Position(100, 730);
            page.Paragraphs.Add(label);

            // Save the PDF (no SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with dynamic list saved to '{outputPath}'.");
    }
}

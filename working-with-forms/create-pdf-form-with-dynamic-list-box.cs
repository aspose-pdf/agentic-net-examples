using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "dynamic_list_form.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x/y, upper‑right x/y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a combo box (editable list) field
            ComboBoxField combo = new ComboBoxField(page, fieldRect);
            combo.PartialName = "DynamicList";

            // Add initial items – Options is read‑only, use AddOption()
            combo.AddOption("Item 1");
            combo.AddOption("Item 2");
            combo.AddOption("Item 3");

            // If the library version supports it, make the combo box editable so users can type new items
            // combo.IsEditable = true; // Uncomment when IsEditable is available

            // Visual styling – Border requires the parent annotation and has no Color property
            combo.Border = new Border(combo) { Width = 1 };
            // Set the border colour (the Border class does not expose a colour property)
            combo.Color = Aspose.Pdf.Color.Black;

            // Add the field to the document's form collection
            doc.Form.Add(combo);

            // Save the PDF with the form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form saved to '{outputPath}'.");
    }
}

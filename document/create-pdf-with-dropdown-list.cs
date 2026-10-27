using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Define the dropdown options
        string[] colors = { "Red", "Green", "Blue", "Yellow", "Black" };

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the dropdown will appear (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a ComboBox (dropdown) form field
            ComboBoxField comboBox = new ComboBoxField(page, comboRect)
            {
                PartialName = "ColorSelection",   // internal name of the field
                Value = colors[0]                 // default selected value
            };

            // Populate the dropdown list with the array values using AddOption
            foreach (string color in colors)
            {
                comboBox.AddOption(color);
            }

            // Add the ComboBox to the document's form collection
            doc.Form.Add(comboBox, 1);

            // Save the PDF to disk
            doc.Save("form_dropdown.pdf");
        }

        Console.WriteLine("PDF with dropdown list created: form_dropdown.pdf");
    }
}

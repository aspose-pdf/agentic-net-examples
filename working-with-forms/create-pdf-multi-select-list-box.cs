using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiSelectListBox.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 500);

            // Create a ListBoxField that allows multiple selections
            ListBoxField listBox = new ListBoxField(page, rect);
            listBox.PartialName = "MultiSelectList";
            listBox.MultiSelect = true; // enable multi‑select

            // Add selectable options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");

            // (Optional) Pre‑select some items by index
            // listBox.SelectedIndices.Add(0); // selects "Option 1"
            // listBox.SelectedIndices.Add(2); // selects "Option 3"

            // Register the field with the document's AcroForm (page number is 1)
            doc.Form.Add(listBox, 1);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑select list box saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiSelectListBoxForm.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the rectangle (llx, lly, urx, ury) where the list box will appear
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a ListBox form field
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multi‑select so users can choose more than one option
                MultiSelect = true,

                // Optional: give the field a name (used when extracting form data)
                PartialName = "MultiSelectOptions"
            };

            // Add options to the list box using AddOption (Option class has no public ctor)
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Pre‑select "Option B" and "Option C" (indices are zero‑based)
            listBox.SelectedItems = new int[] { 1, 2 };

            // Add the list box to the document's form collection
            doc.Form.Add(listBox);

            // Save the PDF; no SaveOptions needed because we are saving as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with multi‑select list box saved to '{outputPath}'.");
    }
}

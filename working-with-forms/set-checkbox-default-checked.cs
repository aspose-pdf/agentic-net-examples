using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // Required for form field classes

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page, 1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the checkbox will appear (llx, lly, urx, ury)
            // Use Aspose.Pdf.Rectangle directly – no need for Aspose.Pdf.Drawing namespace
            Rectangle rect = new Rectangle(100, 700, 120, 720);

            // Create a checkbox form field on the page
            CheckboxField checkBox = new CheckboxField(page, rect)
            {
                // The name used to reference the field programmatically
                PartialName = "MyCheckBox",

                // Set the default state to checked
                Checked = true,

                // The value that represents the checked state (optional, commonly "On")
                Value = "On"
            };

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkBox);

            // Save the PDF; Document.Save writes PDF by default
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checked checkbox saved to '{outputPath}'.");
    }
}
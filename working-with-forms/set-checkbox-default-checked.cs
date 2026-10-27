using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to host the checkbox
            Page page = doc.Pages.Add();

            // Define the checkbox field rectangle (llx, lly, urx, ury)
            CheckboxField checkBox = new CheckboxField(page, new Rectangle(100, 700, 120, 720))
            {
                PartialName = "myCheckBox",
                // Set the default state to checked
                Checked = true,
                // Value exported when the box is checked (property name is ExportValue)
                ExportValue = "Yes"
            };

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a checked checkbox saved to '{outputPath}'.");
    }
}

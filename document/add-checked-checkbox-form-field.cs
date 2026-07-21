using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle for the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true;          // or: checkbox.Value = "On";

            // Optionally set a name for the field (used for form data)
            checkbox.Name = "MyCheckBox";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checked checkbox saved to '{outputPath}'.");
    }
}
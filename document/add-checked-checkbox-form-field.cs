using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_checked.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the checkbox will appear
            // (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the specified page and rectangle
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true; // alternatively: checkbox.Value = "On";

            // Assign a name to the field (optional but useful)
            checkbox.Name = "MyCheckBox";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a checked checkbox saved to '{outputPath}'.");
    }
}
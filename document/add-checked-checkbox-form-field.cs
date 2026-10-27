using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox.pdf";

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the checkbox bounds (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the checkbox form field on the page
            CheckboxField checkBox = new CheckboxField(page, rect);
            checkBox.PartialName = "MyCheckBox";
            checkBox.Checked = true; // set default state to checked

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkBox);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with checkbox saved to '{outputPath}'.");
    }
}
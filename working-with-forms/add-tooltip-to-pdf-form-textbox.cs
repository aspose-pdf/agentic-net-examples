using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the form field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box form field
            TextBoxField dateField = new TextBoxField(page, fieldRect);
            dateField.PartialName = "DateField";                     // internal name
            dateField.Value       = string.Empty;                    // initial value
            // Use AlternateName to provide a tooltip/hover guidance
            dateField.AlternateName = "Enter date as MM/DD/YYYY";   // tooltip guidance

            // Add the field to the document's form on page 1
            doc.Form.Add(dateField, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with tooltip saved to '{outputPath}'.");
    }
}

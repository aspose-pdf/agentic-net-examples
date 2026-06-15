using System;
using System.IO;
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

            // Define the position and size of the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a checkbox field on the specified page
            CheckboxField checkbox = new CheckboxField(page, rect);

            // Set the default state to checked
            checkbox.Checked = true; // Equivalent to setting Value = "On"

            // Assign a name to the field (optional but useful)
            checkbox.Name = "AcceptTerms";

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // needed for Border

class Program
{
    static void Main()
    {
        const string outputPath = "checkbox_styled.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the checkbox will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 120, 620);

            // Create the checkbox field on the page
            CheckboxField checkbox = new CheckboxField(page, rect);
            // Set the visual style of the check mark (e.g., Cross)
            checkbox.Style = BoxStyle.Cross;
            // Set an export value and default checked state
            checkbox.ExportValue = "Option1";
            checkbox.Checked = true; // initially checked

            // OPTIONAL: customize the annotation border appearance
            // Border requires the parent annotation and has no Color property.
            // Border color is controlled by the annotation's own Color property.
            checkbox.Border = new Border(checkbox) { Width = 1 };
            checkbox.Color = Aspose.Pdf.Color.Black;

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkbox);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with styled checkbox saved to '{outputPath}'.");
    }
}

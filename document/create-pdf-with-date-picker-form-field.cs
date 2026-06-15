using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_form.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear.
            // Use the fully qualified Aspose.Pdf.Rectangle to avoid ambiguity.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 630);

            // Create a DateField on the specified page and rectangle.
            DateField dateField = new DateField(page, rect);

            // Set a partial name (field identifier) and an alternate name (tooltip).
            dateField.PartialName = "DateOfBirth";
            dateField.AlternateName = "Select your date of birth";

            // Optional: specify the display format for the date.
            dateField.DateFormat = "MM/dd/yyyy";

            // Add the field to the document's form collection.
            doc.Form.Add(dateField);

            // Save the PDF. No SaveOptions are needed for PDF output.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}
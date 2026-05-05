using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "HeaderDate.pdf";

        // Create a new PDF document and add a single page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define a rectangle for the header (top of the page).
            // Coordinates: llx, lly, urx, ury (points, 1/72 inch).
            Aspose.Pdf.Rectangle headerRect = new Aspose.Pdf.Rectangle(0, 770, 595, 842);

            // Create a DateField that will be placed in the header.
            DateField dateField = new DateField(page, headerRect);
            dateField.PartialName = "dateHeader";          // Field name.
            dateField.IsSharedField = true;                // Appear on all pages.
            dateField.ReadOnly = true;                     // Prevent editing.

            // Add the field to the document's form.
            doc.Form.Add(dateField);

            // JavaScript to set the field value to the current date when the document opens.
            // util.printd formats the date; adjust the format string as needed.
            string js = "var f = this.getField('dateHeader'); " +
                        "f.value = util.printd('mm/dd/yyyy', new Date());";

            // Assign the JavaScript as the document's open action.
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header date saved to '{outputPath}'.");
    }
}
using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "HeaderWithDate.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a single page (the header will be shared across all pages).
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed (top of the page).
            // Rectangle(left, bottom, right, top) – coordinates are in points.
            Aspose.Pdf.Rectangle dateRect = new Aspose.Pdf.Rectangle(
                50,
                page.PageInfo.Height - 50,
                200,
                page.PageInfo.Height - 20);

            // Create the DateField first, then set its properties.
            DateField dateField = new DateField(page, dateRect);
            dateField.Name = "dateField";          // Field name used in JavaScript.
            dateField.IsSharedField = true;        // Appear on every page automatically.
            // Border must be assigned after the field instance exists.
            dateField.Border = new Border(dateField) { Width = 0 }; // No visible border.

            // Add the field to the document's form collection.
            doc.Form.Add(dateField);

            // JavaScript that runs when the document is opened.
            // It retrieves the field by name and sets its value to the current date.
            string js = "var f = this.getField('dateField'); " +
                        "f.value = util.printd('mm/dd/yyyy', new Date());";

            // Assign the JavaScript as the document's OpenAction.
            doc.OpenAction = new JavascriptAction(js);

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header date saved to '{outputPath}'.");
    }
}

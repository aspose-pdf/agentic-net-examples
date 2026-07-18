using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_validation.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create the DateField on the specified page and rectangle
            DateField dateField = new DateField(page, rect);
            dateField.Name = "BirthDate";               // Set a logical name for the field
            dateField.DateFormat = "dd/MM/yyyy";        // Define the display format

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // JavaScript that validates the entered date.
            // It runs when the field is validated (e.g., on exit) and rejects dates before 01/01/2000.
            string js = @"
                var minDate = util.scand('dd/MM/yyyy', '01/01/2000');
                var entered = util.scand('dd/MM/yyyy', event.value);
                if (entered < minDate) {
                    app.alert('Date must be on or after 01/01/2000');
                    event.rc = false; // reject the change
                }
            ";
            JavascriptAction jsAction = new JavascriptAction(js);
            // Use the correct action property – OnValidate – for field validation
            dateField.Actions.OnValidate = jsAction;

            // Set an initial valid date (optional)
            dateField.Value = new DateTime(2000, 1, 1);

            // Save the PDF to disk
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}

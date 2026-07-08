using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                // Set a name for the field (used in JavaScript)
                Name = "BirthDate",
                // Optional: set a default date format (dd/MM/yyyy is default)
                DateFormat = "MM/dd/yyyy"
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // JavaScript to enforce a minimum date of Jan 1, 2000
            // The script runs on field validation (when the user leaves the field)
            // It parses the entered date, compares it to the minimum, and cancels the entry if invalid.
            string js = @"
                if (event.value) {
                    // Parse the entered date using the field's format
                    var entered = util.scand(event.value, 'MM/dd/yyyy');
                    // Define the minimum allowed date
                    var minDate = util.scand('01/01/2000', 'MM/dd/yyyy');
                    // Compare dates; if entered date is earlier, reject it
                    if (entered < minDate) {
                        app.alert('Date must be on or after January 1, 2000.');
                        event.rc = false; // Cancel the validation
                    }
                }
            ";

            // Attach the JavaScript to the field's OnValidate action
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated DateField saved to '{outputPath}'.");
    }
}
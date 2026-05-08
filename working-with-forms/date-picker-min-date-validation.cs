using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create the DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                // Set a user‑friendly date format
                DateFormat = "dd/MM/yyyy",
                // Optional: set an initial valid date
                Value = new DateTime(2023, 1, 1),
                // Set a name for the field (used in JavaScript)
                Name = "MyDateField"
            };

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // JavaScript to reject dates earlier than 01/01/2000
            // The script runs when the field value changes (on blur)
            string js = @"
                if (event.value) {
                    // Parse the entered date using the field's format
                    var entered = util.scand('dd/MM/yyyy', event.value);
                    // Define the minimum allowed date
                    Date minDate = new Date('2000-01-01');
                    // Compare dates (JavaScript Date objects)
                    if (entered < minDate) {
                        app.alert('Date must be on or after 01/01/2000');
                        // Reject the change
                        event.rc = false;
                    }
                }
            ";

            // Attach the JavaScript action to the field
            dateField.ExecuteFieldJavaScript(new JavascriptAction(js));

            // Save the PDF (wrapped in using ensures proper disposal)
            doc.Save("DateFieldWithValidation.pdf");
        }

        Console.WriteLine("PDF created with date validation.");
    }
}
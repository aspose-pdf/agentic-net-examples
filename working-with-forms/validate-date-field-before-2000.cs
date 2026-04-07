using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to host the date field
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                Name = "BirthDate",
                DateFormat = "dd/MM/yyyy", // default format
                // Optional: set a default value (must be >= 01/01/2000)
                Value = new DateTime(2000, 1, 1)
            };

            // Add JavaScript to validate the entered date
            // The script runs when the field loses focus (on blur)
            // It parses the entered value, compares it to 01/01/2000,
            // shows an alert and cancels the change if the date is earlier.
            string js = @"
                if (event.value) {
                    var entered = util.scand('dd/mm/yyyy', event.value);
                    Date minDate = new Date('2000-01-01');
                    if (entered < minDate) {
                        app.alert('Date must be on or after January 1, 2000.');
                        event.rc = false; // reject the value
                    }
                }
            ";
            JavascriptAction jsAction = new JavascriptAction(js);
            dateField.OnActivated = jsAction; // trigger on field activation (focus loss)

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated DateField saved to '{outputPath}'.");
    }
}
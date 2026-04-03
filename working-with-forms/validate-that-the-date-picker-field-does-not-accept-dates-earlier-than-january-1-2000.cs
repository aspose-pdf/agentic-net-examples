using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;          // JavascriptAction (note the lowercase 's')
using Aspose.Pdf.Forms;               // DateField
using Aspose.Pdf.Drawing;             // Rectangle

class Program
{
    static void Main()
    {
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the page
            DateField dateField = new DateField(page, rect)
            {
                Name = "BirthDate",
                // Set the display format (dd/MM/yyyy is default, but set explicitly)
                DateFormat = "dd/MM/yyyy",
                // Optional: set a default value after Jan 1, 2000
                Value = new DateTime(2000, 1, 2)
            };

            // Add JavaScript validation to reject dates earlier than 01/01/2000
            // The JavaScript runs when the field is validated (e.g., on form submit)
            // event.value contains the entered date as a string in the field's format
            // event.rc = false cancels the validation if the condition fails
            string js = @"
                var minDate = util.scand('dd/MM/yyyy', '01/01/2000');
                var entered = util.scand(this.dateFormat, event.value);
                if (entered < minDate) {
                    app.alert('Date must be on or after January 1, 2000.');
                    event.rc = false;
                }";
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated DateField saved to '{outputPath}'.");
    }
}

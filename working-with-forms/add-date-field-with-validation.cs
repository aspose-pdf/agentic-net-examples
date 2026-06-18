using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the date field on the page
            DateField dateField = new DateField(page, rect);
            doc.Form.Add(dateField);

            // Set the display format for the date (e.g., dd/MM/yyyy)
            dateField.DateFormat = "dd/MM/yyyy";

            // Initialize the field with the current date
            dateField.Value = DateTime.Now;

            // Add JavaScript validation to reject dates earlier than 01/01/2000
            // The script runs when the field is validated (e.g., on form submission)
            // event.value contains the string representation of the entered date
            // event.rc = false cancels the validation if the condition is met
            string js = @"
                Date minDate = new Date('2000-01-01');
                var entered = util.scand('dd/MM/yyyy', event.value);
                if (entered < minDate) {
                    app.alert('Date must be on or after January 1, 2000.');
                    event.rc = false;
                }";
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated DateField saved to '{outputPath}'.");
    }
}
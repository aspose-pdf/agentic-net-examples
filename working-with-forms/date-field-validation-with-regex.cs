using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "datefield_validation.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect);
            // Optional: set the visual date format
            dateField.DateFormat = "MM/dd/yyyy";

            // JavaScript that validates the entered value against MM/DD/YYYY
            string js = @"
                var re = /^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/;
                if (!re.test(this.value)) {
                    app.alert('Invalid date format. Please use MM/DD/YYYY');
                    this.value = '';
                }
            ";

            // Attach the validation script to the field's OnValidate action
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
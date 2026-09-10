using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // (left, bottom, right, top) coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create the date field on the page
            DateField dateField = new DateField(page, rect)
            {
                Name = "DateField1",
                // Set the expected date format (displayed in the field)
                DateFormat = "MM/dd/yyyy"
            };

            // JavaScript to validate the entered value against MM/DD/YYYY pattern
            string js = @"
                var re = /^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/;
                if (!re.test(this.value)) {
                    app.alert('Invalid date format. Please use MM/DD/YYYY.');
                    this.value = '';
                }
            ";

            // Attach the JavaScript validation to the field
            JavascriptAction jsAction = new JavascriptAction(js);
            dateField.ExecuteFieldJavaScript(jsAction);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF
            doc.Save("DateFieldValidated.pdf");
        }

        Console.WriteLine("PDF with validated date field created successfully.");
    }
}
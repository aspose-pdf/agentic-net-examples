using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input (optional) and output PDF
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect);

            // Set the display format of the field (optional, shows as MM/dd/yyyy)
            dateField.DateFormat = "MM/dd/yyyy";

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // JavaScript validation: enforce MM/DD/YYYY using a regular expression
            // The script runs when the field loses focus (OnValidate action)
            string js = @"
if (!/^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/.test(this.value)) {
    app.alert('Invalid date format. Please use MM/DD/YYYY.');
    this.value = '';
}";
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated date field saved to '{outputPath}'.");
    }
}
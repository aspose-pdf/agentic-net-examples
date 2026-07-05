using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "template.pdf";   // existing PDF (can be empty)
        const string outputPdf = "validated_form.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPdf))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPdf);
            }
        }

        // Open the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the date field will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format to MM/dd/yyyy (Aspose uses .NET format strings)
            dateField.DateFormat = "MM/dd/yyyy";

            // JavaScript that validates the entered value against the pattern MM/DD/YYYY
            // The regex checks month (01-12), day (01-31) and a four‑digit year.
            string js = @"
if (!/^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])\/\d{4}$/.test(this.value)) {
    app.alert('Invalid date format. Please use MM/DD/YYYY.');
    this.value = '';
}";

            // Assign the JavaScript to the appropriate action property (OnValidate is valid for form fields)
            dateField.Actions.OnValidate = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with date validation saved to '{outputPdf}'.");
    }
}

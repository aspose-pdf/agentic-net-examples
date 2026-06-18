using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create a DateField on the page
            DateField dateField = new DateField(page, rect);
            doc.Form.Add(dateField);

            // Set the display format of the field (optional, helps the user)
            dateField.DateFormat = "MM/dd/yyyy";

            // Attach a JavaScript action that validates the entered value
            // against the regular expression ^\d{2}/\d{2}/\d{4}$
            // If the value does not match, show an alert and clear the field.
            dateField.Actions.OnValidate = new JavascriptAction(
                "var re = /^\\d{2}\\/\\d{2}\\/\\d{4}$/;" +
                "if (!re.test(this.value)) {" +
                "    app.alert('Invalid date format. Please use MM/DD/YYYY');" +
                "    this.value = '';" +
                "}"
            );

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated date field saved to '{outputPath}'.");
    }
}
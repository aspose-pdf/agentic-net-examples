using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "DateFieldValidated.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Define the rectangle where the date field will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create the DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect);
            dateField.Name = "myDate";
            dateField.AlternateName = "Select a date";
            dateField.DateFormat = "dd/MM/yyyy"; // default format
            dateField.Value = new DateTime(2023, 1, 1); // default value

            // Add JavaScript validation to prevent dates earlier than 01/01/2000
            // The script runs when the field loses focus (OnLostFocus event)
            string js = @"
Date minDate = new Date('2000-01-01');
var fieldVal = this.getField('myDate').value;
if (fieldVal) {
    var enteredDate = util.scand('dd/MM/yyyy', fieldVal);
    if (enteredDate < minDate) {
        app.alert('Date must be on or after January 1, 2000.');
        this.getField('myDate').value = '';
    }
}
";
            // Assign the JavaScript action to the OnLostFocus event
            dateField.Actions.OnLostFocus = new JavascriptAction(js);

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with validated DateField saved to '{outputPath}'.");
    }
}

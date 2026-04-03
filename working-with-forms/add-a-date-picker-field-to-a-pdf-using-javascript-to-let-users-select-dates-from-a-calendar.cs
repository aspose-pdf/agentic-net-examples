using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "date_picker.pdf";

        // Create a new PDF document and add a single page.
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Define the rectangle where the date field will be placed.
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create a DateField (calendar view) on the page.
            DateField dateField = new DateField(page, fieldRect);
            // Add the field to the document's form.
            doc.Form.Add(dateField);
            // Initialize the field (required for JavaScript actions).
            dateField.Init(page);

            // Optional: set the display format for the date.
            dateField.DateFormat = "mm/dd/yyyy";

            // Attach JavaScript that opens the calendar when the field receives focus.
            // The JavaScript uses Acrobat's built‑in ShowDatePicker command.
            dateField.Actions.OnEnter = new JavascriptAction("app.execMenuItem('ShowDatePicker');");

            // Save the PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}
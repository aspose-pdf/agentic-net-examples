using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the date field will appear
            // Parameters: left, bottom, width, height
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);

            // Create a DateField on the first page at the specified rectangle
            DateField dateField = new DateField(doc.Pages[1], rect);

            // Set the display format for the date and time
            dateField.DateFormat = "dd/MM/yyyy HH:mm:ss";

            // Initialize the field with the current date and time
            dateField.Value = DateTime.Now;

            // Add JavaScript that updates the field each time the page is opened
            // The script sets the field value to the current date/time using PDF JavaScript
            JavascriptAction js = new JavascriptAction(
                "event.target.value = util.printd('dd/MM/yyyy HH:mm:ss', new Date());"
            );
            // Assign the script to a valid action property (OnOpenPage) – executed when the page is opened
            dateField.Actions.OnOpenPage = js;

            // Add the field to the document's form
            doc.Form.Add(dateField);

            // Save the modified PDF (lifecycle rule: use the same Document instance)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑updating date field: {outputPath}");
    }
}

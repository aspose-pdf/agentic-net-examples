using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
// Note: The Aspose.Pdf.InteractiveFeatures namespace (required for JavaScriptAction) is not available in the current project configuration.
// If you need the field to update on every view, add a reference to the Aspose.Pdf.InteractiveFeatures assembly and uncomment the code below.
// using Aspose.Pdf.InteractiveFeatures;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF or use a blank PDF
        const string outputPath = "output_with_datefield.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the rectangle where the date field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a DateField on the first page
            DateField dateField = new DateField(doc.Pages[1], rect)
            {
                // Set the display format (optional, default is "dd/MM/yyyy")
                DateFormat = "dd/MM/yyyy HH:mm:ss",
                // Set the initial value to the current date and time
                Value = DateTime.Now
            };

            // Add the field to the form
            doc.Form.Add(dateField);

            // OPTIONAL: JavaScript that updates the field each time the page is opened.
            // This requires the Aspose.Pdf.InteractiveFeatures assembly.
            // string js = $"this.getField('{dateField.FullName}').value = util.printd('dd/MM/yyyy HH:mm:ss', new Date());";
            // doc.OpenAction = new JavaScriptAction(js);

            // Save the modified PDF (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with DateField: {outputPath}");
    }
}

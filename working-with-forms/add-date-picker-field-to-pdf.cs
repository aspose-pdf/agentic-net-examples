using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_datefield.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Choose the page where the date field will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the field (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);

            // Create the DateField, add it to the form and initialize it for the page
            DateField dateField = new DateField(page, rect);
            form.Add(dateField);
            dateField.Init(page);

            // Set a display format for the date (e.g., mm/dd/yyyy)
            dateField.DateFormat = "mm/dd/yyyy";

            // Attach a JavaScript action that opens the built‑in calendar picker
            // The JavaScript command "app.execMenuItem('ShowDatePicker');" triggers the PDF viewer's date picker UI
            dateField.OnActivated = new JavascriptAction("app.execMenuItem('ShowDatePicker');");

            // Optionally set a tooltip (alternate name) for better UX
            dateField.AlternateName = "Select a date";

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Date picker field added and saved to '{outputPdf}'.");
    }
}
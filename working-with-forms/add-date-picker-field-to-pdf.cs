using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_datepicker.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the field rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 250, 620);

            // Create a text box form field on the first page
            TextBoxField dateField = new TextBoxField(doc.Pages[1], fieldRect);
            dateField.PartialName = "DatePicker"; // field name
            dateField.Value = string.Empty;        // initial empty value

            // JavaScript to open the built‑in PDF date picker when the field receives focus
            string jsCode = "app.execMenuItem('ShowDatePicker');";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            // Use the correct action property for focus events
            dateField.Actions.OnReceiveFocus = jsAction;

            // Add the field to the document's form collection
            doc.Form.Add(dateField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with date picker saved to '{outputPath}'.");
    }
}

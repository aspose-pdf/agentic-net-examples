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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text box form field that will hold the date/time
            // Fully qualified Rectangle avoids ambiguity with System.Drawing.Rectangle
            TextBoxField dateField = new TextBoxField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 300, 720) // llx, lly, urx, ury
            );

            dateField.PartialName = "dateField"; // field name used in JavaScript
            dateField.Value = "";               // initial empty value
            doc.Form.Add(dateField);              // add the field to the document

            // Add document-level JavaScript that runs when the PDF is opened.
            // It sets the field's value to the current date and time.
            string js = "this.getField('dateField').value = (new Date()).toString();";
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑updating date/time field saved to '{outputPath}'.");
    }
}

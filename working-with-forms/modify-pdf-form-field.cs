using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core Document API
        Document doc = new Document(inputPdf);

        // Example modification: set a value for a form field named "SampleField"
        const string fieldName = "SampleField";
        // Retrieve the field from the AcroForm and cast to the appropriate type (e.g., TextBoxField)
        var field = doc.Form[fieldName] as TextBoxField;
        if (field != null)
        {
            field.Value = "New Value";
        }
        else
        {
            Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text box.");
        }

        // Save the modified PDF to the output path
        doc.Save(outputPdf);

        Console.WriteLine($"Form‑modified PDF saved to '{outputPdf}'.");
    }
}

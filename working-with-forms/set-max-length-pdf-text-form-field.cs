using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF with form field
        const string outputPdf = "output.pdf"; // PDF after setting MaxLen
        const string fieldName = "MyTextField"; // name of the form field to modify
        const int maxLength = 20;                // desired maximum characters

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field safely.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a text field.");
                return;
            }

            // The MaxLen property is defined on TextBoxField (and its derived types)
            if (field is TextBoxField textBox)
            {
                textBox.MaxLen = maxLength;   // set maximum character length
                Console.WriteLine($"Set MaxLen={maxLength} on field '{fieldName}'.");
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}

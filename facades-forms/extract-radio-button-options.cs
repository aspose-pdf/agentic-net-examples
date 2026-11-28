using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "InputForm.pdf";
        const string outputTxt = "RadioOptions.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Ensure the document actually contains a form
        if (pdfDocument.Form == null || pdfDocument.Form.Fields == null)
        {
            Console.Error.WriteLine("The PDF does not contain any form fields.");
            return;
        }

        using (StreamWriter writer = new StreamWriter(outputTxt, false))
        {
            // Iterate over all fields and process only radio‑button groups
            foreach (Field field in pdfDocument.Form.Fields)
            {
                if (field is RadioButtonField radioButton)
                {
                    // The field name (group identifier)
                    string fieldName = radioButton.FullName ?? radioButton.Name;

                    // Retrieve the list of option values for this radio‑button group
                    // Options is an OptionCollection; convert it to a string[] safely
                    string[] options = radioButton.Options != null
                        ? radioButton.Options.Cast<string>().ToArray()
                        : Array.Empty<string>();

                    // Write "FieldName: option1, option2, ..." to the output file
                    string line = $"{fieldName}: {string.Join(", ", options)}";
                    writer.WriteLine(line);
                }
            }
        }

        Console.WriteLine($"Radio button options have been written to '{outputTxt}'.");
    }
}

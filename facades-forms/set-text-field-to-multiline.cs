using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // required per task specification

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "Comments"
            var field = doc.Form["Comments"];
            if (field == null)
            {
                Console.Error.WriteLine("Form field 'Comments' not found.");
                return;
            }

            // The field must be a TextBoxField to support multiline text
            if (field is TextBoxField txtField)
            {
                // Enable multiline (allows line breaks)
                txtField.Multiline = true;
            }
            else
            {
                Console.Error.WriteLine("Form field 'Comments' is not a text box field.");
                return;
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multiline property set for 'Comments' field. Saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "TimeField"; // name of the form field to update

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the form and the specific field exist
            if (doc.Form != null && doc.Form[fieldName] != null && doc.Form[fieldName] is TextBoxField txtField)
            {
                // Set the field's value (which acts as the default when the document is opened) to the current system time (HH:mm:ss)
                txtField.Value = DateTime.Now.ToString("HH:mm:ss");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
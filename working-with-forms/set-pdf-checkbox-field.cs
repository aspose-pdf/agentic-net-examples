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
        bool check = true; // Input data indicating whether the checkbox should be checked

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Locate the first checkbox field in the form
            CheckboxField checkBox = null;
            foreach (Field field in doc.Form.Fields)
            {
                if (field is CheckboxField cb)
                {
                    checkBox = cb;
                    break;
                }
            }

            if (checkBox != null)
            {
                // Set the checkbox state based on the input data
                checkBox.Checked = check;
                // Alternatively, you could set the value directly:
                // checkBox.Value = check ? "On" : "Off";
            }
            else
            {
                Console.WriteLine("No checkbox field found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
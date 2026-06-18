using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the desired checkbox state
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        bool checkBoxShouldBeChecked = true; // set based on your input data

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Locate the first checkbox field in the form (adjust the search as needed)
            CheckboxField checkbox = doc.Form.Fields
                .FirstOrDefault(f => f is CheckboxField) as CheckboxField;

            if (checkbox == null)
            {
                Console.Error.WriteLine("No checkbox field found in the document.");
                return;
            }

            // Set the checkbox state
            checkbox.Checked = checkBoxShouldBeChecked;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox updated and saved to '{outputPath}'.");
    }
}
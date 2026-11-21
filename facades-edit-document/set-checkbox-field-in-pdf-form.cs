using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_checked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Locate the checkbox field named "TermsAccepted" and set it to checked
        if (doc.Form["TermsAccepted"] is CheckboxField checkBox)
        {
            checkBox.Checked = true; // true = checked, false = unchecked
        }
        else
        {
            Console.Error.WriteLine("Checkbox field 'TermsAccepted' not found or is not a CheckboxField.");
        }

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Checkbox 'TermsAccepted' set to true. Saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Determine the desired checkbox state (default to true)
        bool check = true;
        if (args.Length > 0 && bool.TryParse(args[0], out bool parsed))
            check = parsed;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the checkbox field by its full name (replace "CheckBox1" with the actual field name)
            var checkbox = doc.Form["CheckBox1"] as CheckboxField;
            if (checkbox != null)
            {
                // Set the checkbox state
                checkbox.Checked = check;
            }
            else
            {
                Console.Error.WriteLine("Checkbox field not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
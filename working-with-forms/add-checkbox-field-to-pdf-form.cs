using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Define the checkbox position and size (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a new checkbox field on the document
            CheckboxField checkbox = new CheckboxField(doc, rect);

            // Set the field name
            checkbox.Name = "AgreeTerms";

            // Optional: set default checked state (false = unchecked)
            checkbox.Checked = false;

            // Add the field to the form
            doc.Form.Add(checkbox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added and saved to '{outputPath}'.");
    }
}
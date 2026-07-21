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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the checkbox
            Page page = doc.Pages[1];

            // Define the position and size of the checkbox (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the checkbox field on the specified page
            CheckboxField checkbox = new CheckboxField(page, rect);
            checkbox.Name = "AgreeTerms";                     // field name
            checkbox.AlternateName = "Agree to terms";        // tooltip / alternate name

            // Add the checkbox to the document's form
            doc.Form.Add(checkbox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added and saved to '{outputPath}'.");
    }
}
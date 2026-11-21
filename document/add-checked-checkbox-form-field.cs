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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the checkbox
            Page page = doc.Pages[1];

            // Define the rectangle where the checkbox will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create the checkbox form field
            CheckboxField checkBox = new CheckboxField(page, rect)
            {
                PartialName = "CheckBox1", // internal name of the field
                Checked     = true,        // set default state to checked
                ExportValue = "Yes"        // value exported when checked
            };

            // Add the checkbox to the document's form collection
            doc.Form.Add(checkBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox added and saved to '{outputPath}'.");
    }
}
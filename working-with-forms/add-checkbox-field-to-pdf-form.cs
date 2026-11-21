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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the checkbox (lower‑left x,y and upper‑right x,y)
            Rectangle rect = new Rectangle(100, 700, 120, 720);

            // Create a new checkbox field on the first page
            CheckboxField checkBox = new CheckboxField(doc.Pages[1], rect);
            checkBox.PartialName = "AgreeTerms"; // field name
            checkBox.Checked = true;               // true = checked, false = unchecked

            // Add the field to the document's form collection
            doc.Form.Add(checkBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added and saved to '{outputPath}'.");
    }
}

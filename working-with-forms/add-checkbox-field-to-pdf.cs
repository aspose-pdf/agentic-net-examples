using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Needed for Border class

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the checkbox will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create a new checkbox field on the document
            CheckboxField checkbox = new CheckboxField(doc, rect);
            checkbox.Name = "AgreeTerms";          // Field name
            checkbox.Checked = false;               // Initial state (unchecked)
            checkbox.Color = Aspose.Pdf.Color.Black; // Visual property

            // Border must be created after the checkbox instance exists
            checkbox.Border = new Border(checkbox) { Width = 1 };

            // Add the checkbox to the form
            doc.Form.Add(checkbox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Checkbox field 'AgreeTerms' added and saved to '{outputPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";
        const string fieldName  = "MyTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the form field by name
            if (doc.Form != null && doc.Form[ fieldName ] is TextBoxField textBox)
            {
                // Set horizontal alignment to center
                textBox.TextHorizontalAlignment = HorizontalAlignment.Center;

                // Optionally, also center vertically if desired
                // textBox.TextVerticalAlignment = VerticalAlignment.Center;
            }
            else
            {
                Console.Error.WriteLine($"TextBoxField '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with centered text field to '{outputPath}'.");
    }
}
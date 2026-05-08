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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over form fields to find a text box field
            foreach (Field field in doc.Form)
            {
                if (field is TextBoxField textBox)
                {
                    // Set the background color to LightGray
                    // The Color property of a widget annotation controls its fill color
                    textBox.Color = Aspose.Pdf.Color.LightGray;

                    // Optionally, you can also set the border color to distinguish the field
                    // textBox.Border = new Border(textBox) { Width = 1 };
                    // textBox.Border.Color = Aspose.Pdf.Color.DarkGray;

                    // Assuming only one field needs to be changed; break after first match
                    break;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color of the text field set to LightGray. Saved to '{outputPath}'.");
    }
}
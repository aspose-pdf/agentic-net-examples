using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_centered.pdf";
        const string fieldName = "MyTextField"; // name of the text field to modify

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name
            if (doc.Form != null && doc.Form[fieldName] != null)
            {
                // Cast to TextBoxField (the type that supports text entry)
                if (doc.Form[fieldName] is TextBoxField textBox)
                {
                    // Set the horizontal text alignment to center
                    textBox.TextHorizontalAlignment = HorizontalAlignment.Center;

                    // Define a default appearance for the field (font, size, colour)
                    // Use System.Drawing.Color for the colour argument
                    DefaultAppearance appearance = new DefaultAppearance(
                        FontRepository.FindFont("Arial"), // Aspose.Pdf.Text.Font instance
                        12,                               // font size
                        System.Drawing.Color.Black       // text colour
                    );
                    textBox.DefaultAppearance = appearance;

                    // Optionally, set a default value to see the effect
                    // textBox.Value = "Centered Text";

                    // Save the modified PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Field '{fieldName}' processed. Saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine($"Field '{fieldName}' is not a TextBoxField.");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
            }
        }
    }
}

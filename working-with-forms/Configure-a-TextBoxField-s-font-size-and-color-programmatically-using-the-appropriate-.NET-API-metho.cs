using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class ConfigureTextBoxField
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

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the field will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the TextBoxField (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextBoxField on the selected page
            TextBoxField textBox = new TextBoxField(page, rect)
            {
                // Optional: give the field a name
                PartialName = "SampleTextBox"
            };

            // Configure font size and color via DefaultAppearance.
            // Use System.Drawing.Color because DefaultAppearance expects that type.
            textBox.DefaultAppearance = new DefaultAppearance(
                "Helvetica",          // font name
                14,                    // font size
                System.Drawing.Color.Blue // text color (fully qualified)
            );

            // Add the field to the page's annotation collection
            page.Annotations.Add(textBox);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with configured TextBoxField: {outputPath}");
    }
}
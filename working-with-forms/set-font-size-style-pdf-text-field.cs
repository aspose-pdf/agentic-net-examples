using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the text field will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "MyTextField",
                Value = "Enter text here"
            };

            // Set font name, size, and color via DefaultAppearance
            // Helvetica, 14 points, black color
            txtField.DefaultAppearance = new DefaultAppearance("Helvetica", 14, System.Drawing.Color.Black);

            // Optionally enforce minimum and maximum font sizes for the field
            Field.MinFontSize = 10; // -1 disables checking
            Field.MaxFontSize = 20; // -1 disables checking

            // Add the field to the page's annotation collection
            page.Annotations.Add(txtField);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}
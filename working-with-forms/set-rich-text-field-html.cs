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

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the rich text box will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a RichTextBoxField on the page
            RichTextBoxField richTextField = new RichTextBoxField(page, rect);

            // Set HTML markup to display formatted text inside the field
            richTextField.FormattedValue = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined Text</u>";

            // Add the field to the page's annotations collection
            page.Annotations.Add(richTextField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with rich text field: {outputPath}");
    }
}
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

        // Load the existing PDF (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the rich text box will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Create a RichTextBoxField on the first page
            RichTextBoxField richField = new RichTextBoxField(doc.Pages[1], rect);

            // Set HTML markup as the formatted value (RichTextBoxField.FormattedValue)
            richField.FormattedValue = "<b>Bold Text</b> <i>Italic Text</i> <u>Underlined</u> <font color='red'>Red Text</font>";

            // Add the field to the page's annotation collection
            doc.Pages[1].Annotations.Add(richField);

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rich text field added and saved to '{outputPath}'.");
    }
}
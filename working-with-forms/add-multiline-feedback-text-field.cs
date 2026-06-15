using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

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
            // Define the rectangle where the text field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Create a multiline TextBoxField on page 1
            TextBoxField feedbackField = new TextBoxField(doc.Pages[1], rect);
            feedbackField.PartialName = "Feedback"; // field identifier
            feedbackField.Name = "Feedback";        // annotation name
            feedbackField.Multiline = true;        // allow multiple lines
            feedbackField.MaxLen = 500;            // limit to 500 characters

            // Add the field to the form on page 1
            doc.Form.Add(feedbackField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
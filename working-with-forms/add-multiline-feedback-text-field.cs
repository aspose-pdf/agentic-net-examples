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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the text field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create a multiline TextBoxField named "Feedback"
            TextBoxField feedbackField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Name = "Feedback",
                Multiline = true,
                MaxLen = 500
            };

            // Add the field to the form on page 1
            doc.Form.Add(feedbackField, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Feedback field added and saved to '{outputPath}'.");
    }
}
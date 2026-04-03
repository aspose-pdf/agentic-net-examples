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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the position and size of the text field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create a TextBoxField on page 1
            TextBoxField feedbackField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Multiline = true,   // Allow multiple lines of text
                MaxLen    = 500,    // Limit to 500 characters
                Name      = "Feedback"
            };

            // Add the field to the form with the partial name "Feedback" on page 1
            doc.Form.Add(feedbackField, "Feedback", 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiline 'Feedback' field saved to '{outputPath}'.");
    }
}
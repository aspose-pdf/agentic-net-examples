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
            // Define the position and size of the text field (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 500, 400, 600);

            // Create a multiline text box field on the first page
            TextBoxField feedbackField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Name       = "Feedback", // field name
                Multiline  = true,       // allow multiple lines
                MaxLen     = 500          // limit to 500 characters
            };

            // Add the field to the document's form
            doc.Form.Add(feedbackField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 'Feedback' field saved to '{outputPath}'.");
    }
}
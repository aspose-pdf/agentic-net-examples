using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF to add the field to
        const string outputPath = "output.pdf";  // PDF with the new Feedback field

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the field will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle for the text box (left, bottom, width, height)
            // Adjust coordinates as needed for your layout
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 200);

            // Create a multiline text box field named "Feedback"
            TextBoxField feedbackField = new TextBoxField(page, rect)
            {
                Name = "Feedback",      // field name
                Multiline = true,       // allow multiple lines
                MaxLen = 500            // limit to 500 characters
            };

            // Add the field to the document's form
            doc.Form.Add(feedbackField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiline 'Feedback' field saved to '{outputPath}'.");
    }
}
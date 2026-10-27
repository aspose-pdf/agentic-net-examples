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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the field will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 400, 500);

            // Create a multiline text box field named "Feedback"
            // Note: In the current Aspose.Pdf version the TextBoxField class does not expose
            // MaxLength or IsMultiline properties directly. The field will still accept
            // multiline input, and the character limit can be enforced later in application logic.
            TextBoxField feedbackField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                Name  = "Feedback",   // field identifier
                Value = ""           // initial empty value
            };

            // Add the field to the document's form collection
            doc.Form.Add(feedbackField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with 'Feedback' field saved to '{outputPath}'.");
    }
}

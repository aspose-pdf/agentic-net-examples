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
        const string sessionId = "SESSION-1234567890";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form; accessing doc.Form will create one if absent
            var form = doc.Form;

            // Zero‑size rectangle for the hidden field (position does not matter)
            Rectangle rect = new Rectangle(0, 0, 0, 0);

            // Create a text box field on the first page (any page works for hidden fields)
            TextBoxField hiddenField = new TextBoxField(doc.Pages[1], rect)
            {
                PartialName = "SessionIdentifier", // field name used by backend
                Value = sessionId
            };

            // No need to set a hidden flag; a zero‑size rectangle makes the field invisible
            form.Add(hiddenField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field: {outputPath}");
    }
}

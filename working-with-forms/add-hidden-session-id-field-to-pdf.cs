using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string sessionId  = "ABC123XYZ";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a zero‑size rectangle (position is irrelevant for a hidden field)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a hidden text box field to store the session identifier
            TextBoxField hiddenField = new TextBoxField(doc, rect)
            {
                PartialName = "SessionId",   // field name
                Value       = sessionId,     // store the session identifier
                Flags       = AnnotationFlags.Hidden // make the field invisible
            };

            // Add the field to the document's form
            doc.Form.Add(hiddenField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hidden session field saved to '{outputPath}'.");
    }
}
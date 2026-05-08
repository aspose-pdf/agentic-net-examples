using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string sessionId = "ABC123XYZ";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define a zero‑size rectangle (field will not be visible)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a hidden text box field to store the session identifier
            TextBoxField hiddenField = new TextBoxField(doc, rect)
            {
                PartialName = "SessionId",   // field name
                Value = sessionId,           // store the session identifier
                ReadOnly = true,             // prevent editing
                Flags = AnnotationFlags.Hidden // hide the field in the UI (enum, not int)
            };

            // Add the field to the form on the first page (page indexing is 1‑based)
            doc.Form.Add(hiddenField, "SessionId", 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field: {outputPath}");
    }
}

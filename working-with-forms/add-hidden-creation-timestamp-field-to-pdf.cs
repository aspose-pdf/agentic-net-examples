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
        const string outputPath = "output_with_timestamp.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Define a tiny rectangle for the hidden field (position is irrelevant because it's hidden)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);

            // Create a text box field that will hold the creation timestamp
            TextBoxField timestampField = new TextBoxField(doc, rect)
            {
                Name   = "CreationTimestamp",                     // Field name
                Value  = DateTime.UtcNow.ToString("o"),          // ISO 8601 timestamp
                Flags  = AnnotationFlags.Hidden,                 // Make the field hidden
                ReadOnly = true                                 // Prevent editing
            };

            // Add the field to the document's form
            doc.Form.Add(timestampField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden timestamp field: '{outputPath}'.");
    }
}
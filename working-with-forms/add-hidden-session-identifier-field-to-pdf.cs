using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF
        const string outputPath = "output_with_hidden.pdf"; // result PDF
        const string sessionId  = "ABC123-SESSION-ID"; // value to store

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the hidden field will be placed (first page)
            Page page = doc.Pages[1];

            // Define a tiny rectangle; its visual size is irrelevant because the field will be hidden
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 1, 1);

            // Create a text box field (inherits from Field)
            TextBoxField hiddenField = new TextBoxField(page, rect)
            {
                // Set a unique name for backend lookup
                PartialName = "SessionIdentifier",
                // Store the session identifier
                Value = sessionId,
                // Make the field read‑only so it cannot be edited by the user
                ReadOnly = true,
                // Hide the field from the viewer (Annotation flag)
                Flags = AnnotationFlags.Hidden
            };

            // Add the field to the document's form
            doc.Form.Add(hiddenField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Hidden session field added. Saved to '{outputPath}'.");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_hidden.pdf"; // result PDF
        const string sessionId  = "ABC123-SESSION-ID"; // value to store

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Create a hidden text box field to hold the session identifier.
            // Rectangle with zero size makes the field invisible on the page.
            TextBoxField hiddenField = new TextBoxField(
                doc,
                new Aspose.Pdf.Rectangle(0, 0, 0, 0)   // zero‑size rectangle
            );

            hiddenField.PartialName = "SessionId";      // field name
            hiddenField.Value      = sessionId;        // store the session identifier
            hiddenField.ReadOnly   = true;            // prevent user editing

            // Mark the field as hidden so it does not appear in the UI.
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the field to the document's form.
            doc.Form.Add(hiddenField);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string sessionId = "ABC123DEF456"; // session identifier to store

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form; Aspose creates it lazily if it does not exist
            Form form = doc.Form;

            // Create a hidden text box field on the first page
            TextBoxField hiddenField = new TextBoxField(
                doc.Pages[1],                     // first page (1‑based indexing)
                new Rectangle(0, 0, 0, 0))        // zero‑size rectangle (invisible)
            {
                PartialName = "SessionId",      // field name
                Value = sessionId                 // store the session identifier
            };

            // Hide the field from viewers using the AnnotationFlags.Hidden flag
            hiddenField.Flags = AnnotationFlags.Hidden;

            // Add the hidden field to the document's form
            form.Add(hiddenField);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with hidden session field to '{outputPath}'.");
    }
}

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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document.
        Document pdfDocument = new Document(inputPath);

        // Create a hidden text field named "Version" on page 1.
        // The rectangle is zero‑size because the field should not be visible.
        TextBoxField versionField = new TextBoxField(
            pdfDocument.Pages[1],
            new Rectangle(0, 0, 0, 0));
        versionField.PartialName = "Version"; // set field name
        versionField.Value = "2"; // numeric revision

        // Hide the field by setting the NoView flag (field will not be displayed in PDF viewers).
        versionField.Flags = AnnotationFlags.NoView;

        // Add the field to the document's form collection.
        pdfDocument.Form.Add(versionField);

        // Save the modified PDF to a new file.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF with hidden numeric field saved to '{outputPath}'.");
    }
}
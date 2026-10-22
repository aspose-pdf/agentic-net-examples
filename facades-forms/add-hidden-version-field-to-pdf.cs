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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Create a zero‑size rectangle for the hidden field (page 1 is used as a container)
        Rectangle fieldRect = new Rectangle(0, 0, 0, 0);

        // Create a numeric (textbox) field named "Version" with value "2"
        TextBoxField versionField = new TextBoxField(pdfDocument.Pages[1], fieldRect)
        {
            PartialName = "Version",
            Value = "2"
        };

        // Mark the field as hidden by setting the widget annotation flag directly
        versionField.Flags = AnnotationFlags.Hidden;

        // Add the field to the PDF form
        pdfDocument.Form.Add(versionField);

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Hidden numeric field 'Version' added. Saved to '{outputPath}'.");
    }
}

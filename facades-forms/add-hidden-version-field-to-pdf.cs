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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Zero‑size rectangle – position does not matter for a hidden field
        Rectangle rect = new Rectangle(0, 0, 0, 0);

        // Create a numeric field named "Version" with value 2
        NumberField versionField = new NumberField(doc.Pages[1], rect)
        {
            PartialName = "Version",
            Value = "2"
        };

        // Mark the field as hidden using AnnotationFlags
        versionField.Flags = versionField.Flags | AnnotationFlags.Hidden;

        // Add the field to the form (first page)
        doc.Form.Add(versionField, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Hidden numeric field \"Version\" added to '{outputPath}'.");
    }
}

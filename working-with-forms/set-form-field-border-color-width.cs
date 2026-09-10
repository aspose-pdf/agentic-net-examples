using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // required for Border class

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF with form fields
        const string outputPath = "styled_output.pdf"; // result PDF
        const string fieldName = "MyTextField"; // name of the field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (dispose automatically)
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name – the Form indexer returns a WidgetAnnotation,
            // which can be treated as a generic Field.
            Field? field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Corporate style: 2‑point solid blue border.
            // For form fields the Border property expects an Aspose.Pdf.Annotations.Border instance.
            // The Border constructor requires the parent annotation (the field itself).
            field.Border = new Border(field) { Width = 2 }; // Width is an int, not a float
            // Border colour is defined by the field's own Color property, not by BorderInfo.
            field.Color = Aspose.Pdf.Color.Blue;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field '{fieldName}' styled and saved to '{outputPath}'.");
    }
}

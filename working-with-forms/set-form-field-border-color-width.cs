using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";      // source PDF with form fields
        const string outputPath = "styled_form.pdf"; // destination PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Replace "MyTextField" with the actual field name in your PDF.
            const string fieldName = "MyTextField";

            // Retrieve the field; cast to Field because the indexer may return a WidgetAnnotation.
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Set corporate style: red border color, 2‑point width.
            // Border requires the parent annotation (the field itself) in its constructor.
            field.Border = new Border(field) { Width = 2 };
            // Border colour is controlled by the annotation's Color property (no BorderColor property).
            field.Color = Color.Red;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form field border updated and saved to '{outputPath}'.");
    }
}

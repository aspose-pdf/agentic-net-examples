using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "myTextField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to Field to access field‑specific members
            Field field = doc.Form[fieldName] as Field;
            if (field != null)
            {
                // Mark the existing field as required; validation will fail if left empty
                field.Required = true;
            }
            else
            {
                // Field not found – create a new TextBoxField as an example
                // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
                var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 530);
                var txt = new TextBoxField(doc, rect)
                {
                    PartialName = fieldName,
                    Required = true
                };
                // Add the new field to the form (and to the first page annotations)
                doc.Form.Add(txt);
                doc.Pages[1].Annotations.Add(txt);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with required field saved to '{outputPath}'.");
    }
}

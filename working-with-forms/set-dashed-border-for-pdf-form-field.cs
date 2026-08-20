using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF with form fields
        const string outputPath = "output.pdf"; // PDF after styling
        const string fieldName = "OptionalField"; // name of the form field to style

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field by name using the Fields collection
            Field field = doc.Form?.Fields?.FirstOrDefault(f => f.FullName == fieldName);
            if (field != null)
            {
                // The field derives from Annotation, so we can treat it as such
                Annotation annotation = field as Annotation;
                if (annotation != null)
                {
                    // Create a Border object associated with the annotation
                    Border border = new Border(annotation)
                    {
                        Style = BorderStyle.Dashed, // dashed border style
                        Width = 1                    // optional: border width in points
                    };

                    // Assign the configured border back to the annotation
                    annotation.Border = border;
                }
            }
            else
            {
                Console.Error.WriteLine($"Form field '{fieldName}' not found.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed border to '{outputPath}'.");
    }
}

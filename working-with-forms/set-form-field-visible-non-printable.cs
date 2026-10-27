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
        const string fieldName = "myField";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Retrieve the form field by its name. The Form indexer returns a WidgetAnnotation,
                // so we need to cast it to Field (or a derived field type) before using field members.
                Field? field = doc.Form[fieldName] as Field;
                if (field == null)
                {
                    Console.Error.WriteLine($"Form field '{fieldName}' not found or is not a form field.");
                }
                else
                {
                    // Make the field visible on screen but exclude it from printing.
                    // Clear Hidden, Invisible and Print flags.
                    field.Flags &= ~AnnotationFlags.Hidden;      // ensure not hidden
                    field.Flags &= ~AnnotationFlags.Invisible;   // ensure not invisible
                    field.Flags &= ~AnnotationFlags.Print;       // prevent printing
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "form.pdf";
        const string outputPath = "updated.pdf";
        const string fieldName = "myField"; // replace with the actual field name in the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Create a FormEditor instance and bind it to the source PDF
            var formEditor = new FormEditor();
            formEditor.BindPdf(inputPath);

            // AnnotationFlags does not have a 'Visible' member. Visibility is the default state.
            // Use 'Print' to make the field printable and optionally combine with 'Default'.
            var flags = AnnotationFlags.Print | AnnotationFlags.Default;

            // Apply the appearance flags to the existing form field
            formEditor.SetFieldAppearance(fieldName, flags);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf.Facades;
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

        // FormEditor is the correct facade for form manipulation.
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Set the appearance of the field named "Price".
            // The overload takes the field name and an AnnotationFlags value.
            formEditor.SetFieldAppearance("Price", AnnotationFlags.Print);

            // Save the updated PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field \"Price\" appearance set and saved to '{outputPath}'.");
    }
}

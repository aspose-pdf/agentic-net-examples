using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output26.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so we can use a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Set the appearance of the field named "PhoneNumber" to Hidden.
            // The correct enum is AnnotationFlags (Aspose.Pdf.Annotations), not FieldAppearance.
            formEditor.SetFieldAppearance("PhoneNumber", AnnotationFlags.Hidden);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with hidden field to '{outputPath}'.");
    }
}

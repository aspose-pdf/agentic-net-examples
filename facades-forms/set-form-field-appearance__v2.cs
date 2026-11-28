using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "feedback_form_v2.pdf";
        const string outputPath = "comments_appearance.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a Facade class for form manipulation.
        FormEditor formEditor = new FormEditor();

        // Load the PDF into the editor.
        formEditor.BindPdf(inputPath);

        // Set appearance flags for the "Comments" field.
        // We make the field printable. Visibility is the default, so we omit a non‑existent flag.
        formEditor.SetFieldAppearance("Comments", AnnotationFlags.Print);

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}

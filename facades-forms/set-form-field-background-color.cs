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

        // Load the PDF using the Facades API
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPath);

        // Set the appearance of the field named "Status".
        // The SetFieldAppearance method in Aspose.Pdf.Facades only supports a two‑argument overload:
        //   SetFieldAppearance(string fieldName, AnnotationFlags flags)
        // Here we use the Print flag as an example. Background color cannot be set via this overload.
        formEditor.SetFieldAppearance("Status", AnnotationFlags.Print);

        // Save the modified PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Field 'Status' appearance updated. Saved to '{outputPath}'.");
    }
}

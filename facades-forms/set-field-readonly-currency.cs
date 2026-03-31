using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize FormEditor with input and output PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);

        // Set the field "TotalAmount" to read‑only
        bool readOnlyResult = formEditor.SetFieldAttribute("TotalAmount", PropertyFlag.ReadOnly);

        // Set appearance flags for the field (example uses Print flag; replace with appropriate flag for currency formatting if needed)
        bool appearanceResult = formEditor.SetFieldAppearance("TotalAmount", AnnotationFlags.Print);

        // Persist changes to the output file
        formEditor.Save();

        Console.WriteLine($"ReadOnly set: {readOnlyResult}, Appearance set: {appearanceResult}");
    }
}

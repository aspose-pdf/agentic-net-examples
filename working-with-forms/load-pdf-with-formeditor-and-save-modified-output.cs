using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF with FormEditor, modify forms, and bind to the output file
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPath);

            // Example modification: add a text field (optional)
            // Aspose.Pdf.Rectangle defines the field position (llx, lly, urx, ury)
            // formEditor.AddTextField("SampleField", new Aspose.Pdf.Rectangle(100, 500, 300, 530), "Default value");

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
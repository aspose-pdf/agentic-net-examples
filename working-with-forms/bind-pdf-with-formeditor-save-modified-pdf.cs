using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF with FormEditor, modify if needed, and bind to the output file
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the source PDF document for editing
            formEditor.BindPdf(inputPdf);

            // Example: add a text field (optional)
            // formEditor.AddTextField("SampleField", new Aspose.Pdf.Rectangle(100, 500, 300, 550));

            // Save the modified PDF to the output path
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
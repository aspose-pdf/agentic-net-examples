using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Use FormEditor (the correct facade) and dispose it properly
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Copy the outer definition (appearance) of the existing field "Signature"
            // to a new field named "SignatureCopy" on the same page.
            editor.CopyOuterField("Signature", "SignatureCopy");

            // Save the modified PDF
            editor.Save(outputPath);
        }
    }
}
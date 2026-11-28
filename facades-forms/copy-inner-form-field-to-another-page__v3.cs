using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "final.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF document into the FormEditor.
            formEditor.BindPdf(inputPath);

            // Copy the existing inner field "Address" to a new field "AddressCopy"
            // on page 5 of the same document.
            formEditor.CopyInnerField("Address", "AddressCopy", 5);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}

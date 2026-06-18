using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works with a source PDF and a destination PDF.
        // Here we copy the field within the same document, saving the result to a new file.
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Duplicate the field named "Signature" as "SignatureCopy" on the same page.
            // pageNum = -1 means the new field will be placed on the original page.
            formEditor.CopyInnerField("Signature", "SignatureCopy", -1);
            formEditor.Save();
        }

        Console.WriteLine($"Duplicated field saved to '{outputPath}'.");
    }
}
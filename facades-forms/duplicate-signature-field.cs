using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Copy the existing "Signature" field to a new field named "SignatureCopy" on the same page
            formEditor.CopyInnerField("Signature", "SignatureCopy", -1);
            formEditor.Save();
        }

        Console.WriteLine($"Field duplicated. Output saved to '{outputPath}'.");
    }
}
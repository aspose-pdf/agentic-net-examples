using System;
using System.IO;
using Aspose.Pdf;
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

        try
        {
            using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
            {
                // Copy the inner content of "SourceNotes" to a new field "TargetNotes" on the same page
                formEditor.CopyInnerField("SourceNotes", "TargetNotes", -1);
                formEditor.Save();
            }

            Console.WriteLine($"Field copied. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

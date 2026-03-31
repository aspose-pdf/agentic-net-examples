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

        using (Document doc = new Document(inputPath))
        {
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Fade transition is represented by the DISSOLVE constant
                editor.TransitionType = PdfPageEditor.DISSOLVE;
                // Duration of the transition in seconds
                editor.TransitionDuration = 2;
                editor.ApplyChanges();
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with fade transition to '{outputPath}'.");
    }
}

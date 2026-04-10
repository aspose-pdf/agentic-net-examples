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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the PdfPageEditor facade
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Apply the Dissolve transition to page 5 with a 3‑second duration
        editor.ProcessPages = new int[] { 5 };          // target page(s)
        editor.TransitionType = PdfPageEditor.DISSOLVE; // dissolve effect
        editor.TransitionDuration = 3;                  // duration in seconds

        // Commit the changes and save the result
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
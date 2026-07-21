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

        // Initialize the PdfPageEditor facade and bind the source PDF
        PdfPageEditor editor = new PdfPageEditor();
        editor.BindPdf(inputPath);

        // Edit only page 5
        editor.ProcessPages = new int[] { 5 };

        // Set Dissolve transition with a 3‑second duration
        editor.TransitionType = PdfPageEditor.DISSOLVE;
        editor.TransitionDuration = 3;

        // Apply the changes and save the modified PDF
        editor.ApplyChanges();
        editor.Save(outputPath);
        editor.Close();

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}
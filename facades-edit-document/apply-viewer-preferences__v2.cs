using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string[] pdfFiles = new string[] { "input1.pdf", "input2.pdf", "input3.pdf" };
        int viewerPreference = ViewerPreference.HideMenubar;

        foreach (string inputPath in pdfFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            string outputPath = Path.GetFileNameWithoutExtension(inputPath) + "_out.pdf";

            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPath);
            editor.ChangeViewerPreference(viewerPreference);
            editor.Save(outputPath);
        }

        Console.WriteLine("Viewer preferences applied.");
    }
}
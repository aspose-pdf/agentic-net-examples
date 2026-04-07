using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputDirectory = "pdfs";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Switch to the target folder so that Save uses a simple filename
        Directory.SetCurrentDirectory(inputDirectory);

        string[] pdfFiles = Directory.GetFiles(".", "*.pdf");
        foreach (string filePath in pdfFiles)
        {
            string fileName = Path.GetFileName(filePath);

            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(fileName);
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);
            editor.Save(fileName);
            editor.Close();

            Console.WriteLine($"Updated viewer preference to full‑screen for: {fileName}");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_with_title.pdf";
        const string outputPath = "output.pdf";
        const string title      = "Sample Document Title";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 1. Set the document title using the core API and save to a temporary file.
        using (Document doc = new Document(inputPath))
        {
            doc.SetTitle(title);
            doc.Save(tempPath);
        }

        // 2. Change the viewer preference so the window title bar shows the document title.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);
        editor.ChangeViewerPreference(ViewerPreference.DisplayDocTitle);
        editor.Save(outputPath);
        editor.Close();

        // 3. Clean up the temporary file.
        File.Delete(tempPath);

        Console.WriteLine($"PDF saved with title displayed in window caption: {outputPath}");
    }
}
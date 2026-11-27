using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string tempPath = "temp.pdf";
        const string outputPath = "ToolbarHidden.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();               // adds the first (blank) page
            doc.Save(tempPath);            // save to a temporary file for editing
        }

        // Change viewer preference to hide the toolbar using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempPath);                         // load the temporary PDF
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
        editor.Save(outputPath);                           // save the final PDF

        // Clean up the temporary file
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }

        Console.WriteLine($"PDF saved with hidden toolbar: {outputPath}");
    }
}
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // 1. Create a simple PDF in memory (so we don't depend on an external file).
        using (var sourceStream = new MemoryStream())
        {
            // Build a minimal PDF document.
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(sourceStream);
            sourceStream.Position = 0; // reset for reading

            // 2. Bind the in‑memory PDF to PdfContentEditor.
            var editor = new PdfContentEditor();
            editor.BindPdf(sourceStream);

            // 3. Combine the desired viewer preferences using bitwise OR.
            int combinedPreferences = ViewerPreference.CenterWindow | ViewerPreference.HideToolbar;
            editor.ChangeViewerPreference(combinedPreferences);

            // 4. Save the modified PDF to the output file.
            editor.Save(outputPath);
        }
    }
}

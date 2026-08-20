using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "minimalistic.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Initialize the facade that works with viewer preferences
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc); // bind the in‑memory document

            // Combine the two flags using bitwise OR
            int prefs = ViewerPreference.HideToolbar | ViewerPreference.HideMenubar;

            // Apply the viewer preferences (HideToolbar and HideMenubar)
            editor.ChangeViewerPreference(prefs);

            // Save the modified document (lifecycle rule: save within using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved minimalistic PDF to '{outputPath}'.");
    }
}
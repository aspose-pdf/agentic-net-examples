using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "slideshow.pdf";

        using (Document doc = new Document())
        {
            // Slide 1 – Fade transition, 3 seconds display
            Page page1 = doc.Pages.Add();
            page1.Paragraphs.Add(new TextFragment("Slide 1"));

            // Slide 2 – Wipe transition, 5 seconds display
            Page page2 = doc.Pages.Add();
            page2.Paragraphs.Add(new TextFragment("Slide 2"));

            // Slide 3 – Split transition, 4 seconds display
            Page page3 = doc.Pages.Add();
            page3.Paragraphs.Add(new TextFragment("Slide 3"));

            // Apply transitions. Do NOT dispose the PdfPageEditor before the document is saved.
            ApplyTransition(doc, 1, 11, 3); // Fade (value 11), 3 seconds
            ApplyTransition(doc, 2, 4, 5);  // Wipe (value 4), 5 seconds
            ApplyTransition(doc, 3, 1, 4);  // Split (value 1), 4 seconds

            // Save the final PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms.
            SaveDocument(doc, outputPath);
        }

        Console.WriteLine("Slideshow PDF processing completed.");
    }

    private static void ApplyTransition(Document doc, int pageNumber, int transitionType, int durationSeconds)
    {
        // Create PdfPageEditor without a using‑statement so it is not disposed before the document is saved.
        PdfPageEditor editor = new PdfPageEditor(doc);
        editor.ProcessPages = new int[] { pageNumber }; // 1‑based page index
        editor.TransitionType = transitionType;        // integer representing the transition
        editor.TransitionDuration = durationSeconds;   // duration in seconds
        editor.ApplyChanges();
        // Do NOT call editor.Dispose() here; the editor will be disposed automatically when the Document is disposed.
    }

    private static void SaveDocument(Document doc, string path)
    {
        // On Windows the native GDI+ library is always present, so we can save directly.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
            return;
        }

        // On macOS / Linux the save operation may require libgdiplus. Handle the situation gracefully.
        try
        {
            doc.Save(path);
            Console.WriteLine($"PDF saved to '{path}'.");
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                              "The PDF could not be saved using Document.Save(). " +
                              "Consider installing libgdiplus or running the code on Windows.");
        }
    }

    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}

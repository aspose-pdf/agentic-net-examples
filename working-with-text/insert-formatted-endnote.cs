using System;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output_with_endnote.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a text fragment that will contain the endnote reference
            TextFragment fragment = new TextFragment("This is a paragraph with an endnote.");

            // Create the endnote (Note) object
            Note endNote = new Note("This is the formatted endnote text.");

            // Define a TextState with bold and italic styles
            TextState noteTextState = new TextState("Helvetica", true, true);

            // Apply the TextState to the endnote
            endNote.TextState = noteTextState;

            // Attach the endnote to the text fragment
            fragment.EndNote = endNote;

            // Add the fragment (with its endnote) to the page
            page.Paragraphs.Add(fragment);

            // Save the PDF – guard against missing GDI+ (libgdiplus) on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. PDF saved without GDI+ dependent features.");
                }
            }
        }

        Console.WriteLine($"PDF creation attempt finished. Check '{outputPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
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
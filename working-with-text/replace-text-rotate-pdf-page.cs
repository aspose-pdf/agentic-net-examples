using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // Prepare a PDF document. If an "input.pdf" file exists it will be used;
        // otherwise a simple PDF containing the word "Hello" is created on‑the‑fly.
        // ---------------------------------------------------------------------
        Document doc;
        const string inputPath = "input.pdf";
        if (File.Exists(inputPath))
        {
            // Load the existing PDF from file (wrapped in a MemoryStream for consistency)
            byte[] pdfBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream inputStream = new MemoryStream(pdfBytes))
            {
                doc = new Document(inputStream);
            }
        }
        else
        {
            // Create a minimal PDF with a single page that contains the text "Hello"
            doc = new Document();
            Page page = doc.Pages.Add();
            TextFragment tf = new TextFragment("Hello Aspose PDF!");
            page.Paragraphs.Add(tf);
        }

        // -------------------------------------------------
        // Text replacement: replace all occurrences of "Hello" with "Hi"
        // -------------------------------------------------
        TextFragmentAbsorber absorber = new TextFragmentAbsorber("Hello");
        doc.Pages.Accept(absorber);
        foreach (TextFragment fragment in absorber.TextFragments)
        {
            fragment.Text = "Hi";
        }

        // -------------------------------------------------
        // Page rotation: rotate the first page 90 degrees clockwise
        // -------------------------------------------------
        if (doc.Pages.Count >= 1)
        {
            Page firstPage = doc.Pages[1]; // Pages are 1‑based
            firstPage.Rotate = Rotation.on90; // 90° clockwise
        }

        // -------------------------------------------------
        // Save the modified PDF into a memory stream (with GDI+ guard)
        // -------------------------------------------------
        using (MemoryStream outputStream = new MemoryStream())
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Windows has GDI+ – safe to save directly
                doc.Save(outputStream);
                File.WriteAllBytes("output.pdf", outputStream.ToArray());
            }
            else
            {
                // On non‑Windows platforms GDI+ (libgdiplus) may be missing.
                // Attempt to save and handle the possible TypeInitializationException.
                try
                {
                    doc.Save(outputStream);
                    File.WriteAllBytes("output.pdf", outputStream.ToArray());
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. " +
                                      "The PDF could not be saved.");
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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

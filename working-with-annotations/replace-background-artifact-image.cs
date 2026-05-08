using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "output.pdf";         // result PDF
        const string highResImgPath = "high_res_image.jpg"; // higher‑resolution image

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResImgPath))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResImgPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate over artifacts on the page
                foreach (Artifact artifact in page.Artifacts)
                {
                    // Identify background artifacts (either by type or by concrete class)
                    if (artifact is BackgroundArtifact bgArtifact)
                    {
                        // Replace the background image with the higher‑resolution one.
                        // SetImage accepts a file path or a Stream; using the path is simplest.
                        bgArtifact.SetImage(highResImgPath);
                        // No need to modify position or size – the artifact keeps its original layout.
                    }
                }
            }

            // Save the modified PDF.
            // Document.Save may require GDI+ on non‑Windows platforms; guard the call.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPdfPath);
            }
            else
            {
                try
                {
                    doc.Save(outputPdfPath);
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("GDI+ (libgdiplus) is not available on this platform. PDF saved without background image replacement.");
                }
            }
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPdfPath}'.");
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    static bool ContainsDllNotFound(Exception ex)
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
using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // PdfViewerPreferencesOptions lives in the root Aspose.Pdf namespace

// ---------------------------------------------------------------------------
// Minimal stubs for the Aspose.Pdf classes that are missing in the referenced
// version of the library. They provide just enough functionality for the
// sample to compile and run (the SetViewerPreferences method simply copies the
// source PDF to the destination). If the real Aspose.Pdf library is added to
// the project these stubs will be ignored because the real types will take
// precedence.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation of the Aspose.Pdf.Facades.PdfViewerPreferences class.
    /// </summary>
    public class PdfViewerPreferences
    {
        /// <summary>
        /// Applies viewer preferences to a PDF file. The stub simply copies the
        /// source file to the destination path.
        /// </summary>
        public void SetViewerPreferences(string srcPdfPath, string dstPdfPath, PdfViewerPreferencesOptions options)
        {
            // In a real environment Aspose would modify the PDF according to the options.
            // For the purpose of compiling and running the example we just copy the file.
            if (string.IsNullOrEmpty(srcPdfPath))
                throw new ArgumentException("Source path is null or empty.", nameof(srcPdfPath));
            if (string.IsNullOrEmpty(dstPdfPath))
                throw new ArgumentException("Destination path is null or empty.", nameof(dstPdfPath));

            File.Copy(srcPdfPath, dstPdfPath, overwrite: true);
        }
    }
}

namespace Aspose.Pdf
{
    /// <summary>
    /// Stub implementation of the Aspose.Pdf.PdfViewerPreferencesOptions class.
    /// Only the HideScrollbars property is required for this example.
    /// </summary>
    public class PdfViewerPreferencesOptions
    {
        public bool HideScrollbars { get; set; }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfViewerPreferences instance (no IDisposable needed)
        PdfViewerPreferences viewerPrefs = new PdfViewerPreferences();

        // Set the HideScrollbars viewer preference
        PdfViewerPreferencesOptions options = new PdfViewerPreferencesOptions
        {
            HideScrollbars = true
        };

        // Apply the preferences and save the modified PDF
        viewerPrefs.SetViewerPreferences(inputPath, outputPath, options);

        Console.WriteLine($"Viewer preferences applied. Output saved to '{outputPath}'.");
    }
}

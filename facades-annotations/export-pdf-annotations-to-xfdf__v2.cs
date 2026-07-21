using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Binds a PDF from a memory stream, exports its annotations to XFDF,
    /// and returns the XFDF data as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Byte array containing the PDF document.</param>
    /// <returns>Byte array containing the exported XFDF.</returns>
    public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
    {
        // Input PDF stream
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        // Facade for annotation operations
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfStream);

            // Output XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations to the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Return the XFDF data as a byte array
                return xfdfStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy console‑app compilation
public class Program
{
    public static void Main(string[] args)
    {
        // The Main method is required for a project that builds an executable.
        // It can remain empty or contain sample usage code.
        // Example (commented out):
        // byte[] pdf = File.ReadAllBytes("sample.pdf");
        // byte[] xfdf = PdfAnnotationHelper.ExportAnnotationsToXfdf(pdf);
        // File.WriteAllBytes("output.xfdf", xfdf);
    }
}
using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Binds a PDF from a memory stream, exports all annotations to XFDF,
    /// and returns the XFDF data as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Byte array containing the source PDF.</param>
    /// <returns>Byte array containing the exported XFDF.</returns>
    public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
    {
        // Input PDF stream (memory‑based)
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        // PdfAnnotationEditor implements IDisposable, so wrap in using
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Bind the PDF document to the editor
            editor.BindPdf(pdfStream);

            // Output XFDF stream (memory‑based)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                // Export all annotations into the XFDF stream
                editor.ExportAnnotationsToXfdf(xfdfStream);

                // Return the XFDF content as a byte array
                return xfdfStream.ToArray();
            }
        }
    }
}

// Dummy entry point to satisfy a console‑application build.
public class Program
{
    public static void Main(string[] args)
    {
        // Placeholder – no operation required for the library functionality.
    }
}
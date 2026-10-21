using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

public static class PdfAnnotationHelper
{
    /// <summary>
    /// Binds a PDF from a memory buffer, exports its annotations to XFDF,
    /// and returns the XFDF data as a byte array.
    /// </summary>
    /// <param name="pdfBytes">Byte array containing the source PDF.</param>
    /// <returns>Byte array containing the exported XFDF.</returns>
    public static byte[] ExportAnnotationsToXfdf(byte[] pdfBytes)
    {
        if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

        // Load the PDF from the supplied byte array.
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var document = new Document(pdfStream))
        using (var xfdfStream = new MemoryStream())
        {
            // The XFDF functionality lives in a separate assembly (Aspose.Pdf.Xfdf).
            // To keep the project compile‑time independent of that optional DLL we load it
            // via reflection at runtime. If the assembly is not present we throw a clear
            // exception so the caller knows why the operation cannot be performed.
            const string xfdfAssemblyName = "Aspose.Pdf.Xfdf";
            Assembly xfdfAssembly;
            try
            {
                xfdfAssembly = Assembly.Load(new AssemblyName(xfdfAssemblyName));
            }
            catch (FileNotFoundException)
            {
                throw new InvalidOperationException($"The optional assembly '{xfdfAssemblyName}' is required to export annotations to XFDF but was not found.");
            }

            // Resolve the XfdfWriter type and its static WriteAnnotations method.
            var writerType = xfdfAssembly.GetType("Aspose.Pdf.Xfdf.XfdfWriter", throwOnError: true);
            var writeMethod = writerType.GetMethod(
                "WriteAnnotations",
                BindingFlags.Public | BindingFlags.Static,
                binder: null,
                types: new[] { typeof(Document), typeof(Stream) },
                modifiers: null);

            if (writeMethod == null)
                throw new MissingMethodException("WriteAnnotations method not found in XfdfWriter.");

            // Invoke the method to write XFDF data into the memory stream.
            writeMethod.Invoke(null, new object[] { document, xfdfStream });

            // Return the underlying byte array.
            return xfdfStream.ToArray();
        }
    }
}

// Minimal entry point – required only when the project is built as a console app.
public class Program
{
    public static void Main(string[] args)
    {
        // Example usage (can be removed in production).
        // byte[] pdf = File.ReadAllBytes("sample.pdf");
        // byte[] xfdf = PdfAnnotationHelper.ExportAnnotationsToXfdf(pdf);
        // Console.WriteLine($"Exported XFDF size: {xfdf.Length} bytes");
    }
}
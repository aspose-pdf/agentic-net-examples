using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";          // source PDF containing annotations
        const string xfdfPath = "annotations.xfdf";   // temporary XFDF file
        const string gzPath   = "annotations.xfdf.gz"; // final compressed file

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(pdfPath);

            // ------------------------------------------------------------
            // Export annotations to XFDF.
            // The XFDF functionality lives in a separate assembly
            // (Aspose.Pdf.Xfdf.dll). To avoid a compile‑time dependency we
            // load the assembly via reflection and invoke the static
            // XfdfWriter.WriteAnnotations method.
            // ------------------------------------------------------------
            ExportAnnotationsToXfdf(pdfDocument, xfdfPath);
            Console.WriteLine($"Annotations exported to XFDF: {xfdfPath}");

            // Compress the XFDF file with GZip for efficient storage
            using (FileStream sourceStream = new FileStream(xfdfPath, FileMode.Open, FileAccess.Read))
            using (FileStream targetStream = new FileStream(gzPath, FileMode.Create, FileAccess.Write))
            using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
            {
                sourceStream.CopyTo(compressionStream);
            }
            Console.WriteLine($"Compressed XFDF saved to: {gzPath}");

            // Optional: delete the intermediate uncompressed XFDF file
            try { File.Delete(xfdfPath); } catch { /* ignore cleanup errors */ }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Writes PDF annotations to an XFDF file using reflection to avoid a direct
    /// compile‑time reference to the Aspose.Pdf.Xfdf assembly.
    /// </summary>
    /// <param name="pdfDocument">The loaded PDF document.</param>
    /// <param name="xfdfFilePath">Path where the XFDF file will be created.</param>
    private static void ExportAnnotationsToXfdf(Document pdfDocument, string xfdfFilePath)
    {
        // Attempt to load the optional Aspose.Pdf.Xfdf assembly.
        // It is expected to be located in the same folder as the executable.
        const string xfdfAssemblyName = "Aspose.Pdf.Xfdf";
        Assembly xfdfAssembly;
        try
        {
            xfdfAssembly = Assembly.Load(new AssemblyName(xfdfAssemblyName));
        }
        catch (FileNotFoundException)
        {
            throw new InvalidOperationException($"The optional assembly '{xfdfAssemblyName}.dll' is required to export annotations to XFDF but was not found.");
        }

        // Get the XfdfWriter type (Aspose.Pdf.Xfdf.XfdfWriter)
        Type writerType = xfdfAssembly.GetType("Aspose.Pdf.Xfdf.XfdfWriter", throwOnError: true);
        // Locate the static method: void WriteAnnotations(Document, Stream)
        MethodInfo writeMethod = writerType.GetMethod("WriteAnnotations", BindingFlags.Public | BindingFlags.Static, null, new[] { typeof(Document), typeof(Stream) }, null);
        if (writeMethod == null)
            throw new MissingMethodException("WriteAnnotations method not found in Aspose.Pdf.Xfdf.XfdfWriter.");

        // Execute the method.
        using (FileStream xfdfStream = new FileStream(xfdfFilePath, FileMode.Create, FileAccess.Write))
        {
            writeMethod.Invoke(null, new object[] { pdfDocument, xfdfStream });
        }
    }
}

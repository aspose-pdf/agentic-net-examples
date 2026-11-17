using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF to receive annotations
        const string fdfPath = "annotations.fdf";        // FDF file containing annotations
        const string outputPdfPath = "output_annotated.pdf";

        // Verify that source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Import annotations from the FDF file.
                // The ImportAnnotations API changed in recent Aspose.Pdf versions.
                // We first try the Stream overload; if it does not exist we fall back to the string overload.
                ImportAnnotationsFromFdf(pdfDoc, fdfPath);

                // Save the updated PDF.
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Imports annotations from an FDF file into the provided PDF document.
    /// Handles API differences across Aspose.Pdf versions without using any Facades namespaces.
    /// </summary>
    private static void ImportAnnotationsFromFdf(Document doc, string fdfFilePath)
    {
        // Open the FDF file as a stream – this works for both the Stream overload and the string overload.
        using (FileStream fdfStream = new FileStream(fdfFilePath, FileMode.Open, FileAccess.Read))
        {
            // Try to locate a method with the signature ImportAnnotations(Stream)
            MethodInfo method = typeof(Document).GetMethod("ImportAnnotations", new[] { typeof(Stream) });
            if (method != null)
            {
                // The Stream overload exists – invoke it directly.
                method.Invoke(doc, new object[] { fdfStream });
                return;
            }

            // If the Stream overload is not present, look for ImportAnnotations(string)
            method = typeof(Document).GetMethod("ImportAnnotations", new[] { typeof(string) });
            if (method != null)
            {
                // Invoke the string overload.
                method.Invoke(doc, new object[] { fdfFilePath });
                return;
            }

            // If neither overload is found, the current Aspose.Pdf version does not expose ImportAnnotations.
            throw new NotSupportedException("The current Aspose.Pdf version does not provide an ImportAnnotations method.");
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfHelper
{
    /// <summary>
    /// Loads a PDF file, applies a simple modification (adds a text annotation),
    /// and returns the resulting PDF as a byte array.
    /// </summary>
    /// <param name="inputPath">Full path to the source PDF file.</param>
    /// <returns>Byte array containing the modified PDF.</returns>
    public static byte[] GetModifiedPdfBytes(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This PDF was processed and saved to a byte array.",
                Open     = true,
                Icon     = TextIcon.Note,
                Color    = Aspose.Pdf.Color.Yellow
            };
            doc.Pages[1].Annotations.Add(annotation);

            // Save the document to a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                // Document.Save(Stream) stores the PDF into the provided stream.
                doc.Save(ms);
                // Return the underlying byte array.
                return ms.ToArray();
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Required entry point for a console application.
        // No operation is performed here; the method exists solely to satisfy the compiler.
    }
}
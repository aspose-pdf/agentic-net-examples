using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public static class PdfHelper
{
    /// <summary>
    /// Loads a PDF, applies a simple modification, and returns the result as a byte array.
    /// </summary>
    /// <param name="inputPath">Path to the source PDF file.</param>
    /// <returns>Byte array containing the modified PDF.</returns>
    public static byte[] ModifyPdfAndGetBytes(string inputPath)
    {
        // Ensure the source file exists
        if (!File.Exists(inputPath))
            throw new FileNotFoundException($"Input PDF not found: {inputPath}");

        // Load the document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Example modification: add a text annotation on the first page
            Page page = doc.Pages[1]; // 1‑based indexing
            // Fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Added via Aspose.Pdf",
                Color    = Aspose.Pdf.Color.Yellow, // Use Aspose.Pdf.Color (cross‑platform)
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the modified document to a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                doc.Save(ms); // Document.Save(Stream) stores the PDF in the stream
                return ms.ToArray(); // Return the byte array for further processing or transmission
            }
        }
    }
}

// Minimal entry point to satisfy the compiler when the project is built as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // The method is intentionally left empty. It can be used for quick manual testing, e.g.:
        // if (args.Length > 0) {
        //     var bytes = PdfHelper.ModifyPdfAndGetBytes(args[0]);
        //     Console.WriteLine($"Modified PDF size: {bytes.Length} bytes");
        // }
    }
}
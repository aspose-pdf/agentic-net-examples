using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

namespace PdfUtility
{
    /// <summary>
    /// Helper that loads a PDF, adds a simple text annotation, and returns the modified PDF as a byte array.
    /// </summary>
    public static class PdfByteArrayHelper
    {
        /// <summary>
        /// Loads a PDF, applies a simple modification (adds a text annotation),
        /// and returns the resulting PDF as a byte array.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF file.</param>
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
                TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], rect)
                {
                    Title    = "Note",
                    Contents = "Sample annotation added programmatically.",
                    Open     = true,
                    Color    = Aspose.Pdf.Color.Yellow,
                    Icon     = TextIcon.Note
                };
                doc.Pages[1].Annotations.Add(textAnn);

                // Save the document into a memory stream.
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);               // Uses Document.Save(Stream) overload.
                    // Ensure the stream position is at the beginning before reading.
                    ms.Position = 0;
                    // Return the underlying byte array.
                    return ms.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Minimal entry point required for a console‑type project.
    /// The method is intentionally lightweight; it can be expanded for real‑world usage.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // Example usage (optional). Commented out to keep the method side‑effect free.
            // if (args.Length > 0)
            // {
            //     byte[] pdfBytes = PdfByteArrayHelper.GetModifiedPdfBytes(args[0]);
            //     Console.WriteLine($"Modified PDF size: {pdfBytes.Length} bytes");
            // }
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;

namespace PdfHelper
{
    class PdfByteArrayHelper
    {
        /// <summary>
        /// Loads a PDF document, applies optional modifications, and returns the PDF as a byte array.
        /// </summary>
        /// <param name="inputPath">Path to the source PDF file.</param>
        /// <returns>Byte array containing the saved PDF.</returns>
        public static byte[] SavePdfToByteArray(string inputPath)
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"File not found: {inputPath}");

            // Load the PDF document (lifecycle: create -> load -> save)
            using (Document pdfDocument = new Document(inputPath))
            {
                // -------------------------------------------------
                // Place any PDF modifications here (e.g., add annotations, edit pages)
                // -------------------------------------------------

                // Save the document into a memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    pdfDocument.Save(ms); // Document.Save(Stream) writes PDF bytes to the stream
                    ms.Position = 0;      // Reset position before reading
                    return ms.ToArray();  // Extract the byte array for further processing or transmission
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Optional demonstration of the helper method.
            // Uncomment and provide a valid PDF path to test.
            // if (args.Length > 0)
            // {
            //     byte[] pdfBytes = PdfByteArrayHelper.SavePdfToByteArray(args[0]);
            //     Console.WriteLine($"PDF byte array length: {pdfBytes.Length}");
            // }
        }
    }
}
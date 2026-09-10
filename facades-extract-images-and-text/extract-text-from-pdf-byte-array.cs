using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

namespace AsposePdfDemo
{
    public static class PdfTextExtractor
    {
        /// <summary>
        /// Extracts all text from a PDF supplied as a byte array.
        /// The PDF is processed entirely in memory; no files are written to disk.
        /// </summary>
        /// <param name="pdfBytes">Byte array containing the PDF data.</param>
        /// <returns>The extracted text as a string.</returns>
        public static string ExtractTextFromBytes(byte[] pdfBytes)
        {
            if (pdfBytes == null) throw new ArgumentNullException(nameof(pdfBytes));

            // Wrap the byte array in a MemoryStream for Aspose.Pdf.Facades binding.
            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            // Create the PdfExtractor facade.
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the PDF document from the stream.
                extractor.BindPdf(pdfStream);

                // Perform text extraction using the default Unicode encoding.
                extractor.ExtractText();

                // Capture the extracted text into another MemoryStream.
                using (MemoryStream textStream = new MemoryStream())
                {
                    extractor.GetText(textStream);

                    // Convert the resulting bytes (Unicode) to a .NET string.
                    return Encoding.Unicode.GetString(textStream.ToArray());
                }
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            // Example usage – can be removed or replaced in production code.
            // byte[] pdfBytes = File.ReadAllBytes("sample.pdf");
            // string extracted = PdfTextExtractor.ExtractTextFromBytes(pdfBytes);
            // Console.WriteLine(extracted);
        }
    }
}

using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for DocSaveOptions.RecognitionMode enum

namespace AsposePdfConversion
{
    public class PdfToDocxConverter
    {
        /// <summary>
        /// Converts a PDF file to a DOCX file using explicit save options.
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="docxPath">Full path where the DOCX will be saved.</param>
        public static void Convert(string pdfPath, string docxPath)
        {
            // Verify source file exists
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"Source PDF not found: {pdfPath}");

            // Load the PDF document (lifecycle: create -> load)
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Configure DOCX-specific save options (explicit output intent)
                DocSaveOptions saveOptions = new DocSaveOptions
                {
                    // Specify the desired output format
                    Format = DocSaveOptions.DocFormat.DocX,

                    // Choose a recognition mode; Flow provides maximum editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,

                    // Optional: improve conversion quality
                    RecognizeBullets = true,
                    RelativeHorizontalProximity = 2.5f
                };

                // Save the document as DOCX (must pass SaveOptions for non‑PDF output)
                pdfDocument.Save(docxPath, saveOptions);
            }
        }
    }

    public class Program
    {
        /// <summary>
        /// Entry point required for a console application.
        /// Usage: AsposePdfConversion <sourcePdfPath> <outputDocxPath>
        /// </summary>
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: AsposePdfConversion <sourcePdfPath> <outputDocxPath>");
                return;
            }

            string pdfPath = args[0];
            string docxPath = args[1];

            try
            {
                PdfToDocxConverter.Convert(pdfPath, docxPath);
                Console.WriteLine($"Conversion succeeded: {docxPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

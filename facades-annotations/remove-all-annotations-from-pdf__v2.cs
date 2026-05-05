using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessingConsole
{
    class Program
    {
        /// <summary>
        /// Removes all annotations from a PDF file.
        /// Usage: PdfProcessingConsole.exe <inputPdfPath> <outputPdfPath>
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: PdfProcessingConsole.exe <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                // Load the PDF into a memory stream (optional, but mirrors the original API design)
                using (MemoryStream inputStream = new MemoryStream(File.ReadAllBytes(inputPath)))
                {
                    // Bind the PDF to the annotation editor and delete all annotations
                    using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(inputStream);
                        editor.DeleteAnnotations();

                        // Save the cleaned PDF to the output file
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            editor.Save(outputStream);
                            File.WriteAllBytes(outputPath, outputStream.ToArray());
                        }
                    }
                }

                Console.WriteLine($"Annotations removed successfully. Clean PDF saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

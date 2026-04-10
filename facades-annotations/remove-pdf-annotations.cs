using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessingConsole
{
    class Program
    {
        /// <summary>
        /// Entry point for the console application.
        /// Usage: PdfProcessingConsole <input.pdf> <output.pdf>
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfProcessingConsole <input.pdf> <output.pdf>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Validate input file existence
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Process the PDF: remove all annotations and write the cleaned PDF
            using (FileStream inputStream = File.OpenRead(inputPath))
            using (FileStream outputStream = File.Create(outputPath))
            {
                RemoveAnnotations(inputStream, outputStream);
            }

            Console.WriteLine($"Annotations removed. Clean PDF saved to: {outputPath}");
        }

        /// <summary>
        /// Removes all annotations from a PDF document.
        /// </summary>
        /// <param name="input">Stream containing the original PDF.</param>
        /// <param name="output">Stream where the cleaned PDF will be written.</param>
        public static void RemoveAnnotations(Stream input, Stream output)
        {
            // Aspose.Pdf.Facades.PdfAnnotationEditor provides annotation manipulation capabilities.
            using var editor = new PdfAnnotationEditor();
            editor.BindPdf(input);
            editor.DeleteAnnotations();
            editor.Save(output);
        }
    }
}

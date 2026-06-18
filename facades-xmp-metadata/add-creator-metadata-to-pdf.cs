using System;
using Aspose.Pdf;

namespace PdfExtensions
{
    /// <summary>
    /// Provides extension methods for Aspose.Pdf.Document.
    /// </summary>
    public static class DocumentExtensions
    {
        /// <summary>
        /// Adds or updates the Creator metadata field of the PDF document.
        /// </summary>
        /// <param name="doc">The PDF document to modify.</param>
        /// <param name="creatorTool">The value to set for the Creator field.</param>
        public static void AddCreatorTool(this Document doc, string creatorTool)
        {
            if (doc == null) throw new ArgumentNullException(nameof(doc));
            if (creatorTool == null) throw new ArgumentNullException(nameof(creatorTool));

            // Set the Creator metadata using the DocumentInfo object.
            doc.Info.Creator = creatorTool;
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            const string inputPath = "input.pdf";
            const string outputPath = "output.pdf";
            const string creator = "MyCreatorTool v1.0";

            // Ensure the input file exists before proceeding.
            if (!System.IO.File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            // Load, modify, and save the PDF using proper disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Add the Creator metadata.
                pdfDoc.AddCreatorTool(creator);

                // Save the modified document.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with Creator set to \"{creator}\" at '{outputPath}'.");
        }
    }
}
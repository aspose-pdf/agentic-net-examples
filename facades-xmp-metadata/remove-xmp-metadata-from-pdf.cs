using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfApi
{
    /// <summary>
    /// Utility class that provides PDF metadata manipulation helpers.
    /// </summary>
    public static class PdfMetadataUtility
    {
        /// <summary>
        /// Removes the entire XMP metadata block from the specified PDF file
        /// and saves the result to a new file.
        /// </summary>
        /// <param name="inputPdfPath">Path to the source PDF.</param>
        /// <param name="outputPdfPath">Path where the metadata‑free PDF will be saved.</param>
        public static void RemoveXmpMetadata(string inputPdfPath, string outputPdfPath)
        {
            // Verify that the input file exists
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            // Create the XMP metadata facade, bind the PDF, clear the XMP block, and save.
            // PdfXmpMetadata implements SaveableFacade, which provides BindPdf, Clear, and Save methods.
            PdfXmpMetadata xmp = new PdfXmpMetadata();

            // Load the PDF into the facade
            xmp.BindPdf(inputPdfPath);

            // Remove all XMP elements
            xmp.Clear();

            // Persist the changes to a new file
            xmp.Save(outputPdfPath);
        }
    }

    /// <summary>
    /// Simple console entry point required for a runnable project.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Expected arguments: <c>inputPdfPath outputPdfPath</c>
        /// </summary>
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                Console.WriteLine("Usage: AsposePdfApi <inputPdfPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                PdfMetadataUtility.RemoveXmpMetadata(inputPath, outputPath);
                Console.WriteLine($"Metadata removed successfully. Output saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}

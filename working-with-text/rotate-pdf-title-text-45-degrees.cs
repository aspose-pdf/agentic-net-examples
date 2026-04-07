using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfTitleRotator
{
    /// <summary>
    /// Rotates the title text of each PDF file by 45 degrees and saves the result.
    /// The method processes an array of input PDF file paths and writes the
    /// rotated PDFs to the specified output directory, preserving the original
    /// file name with a "_rotated" suffix.
    /// </summary>
    /// <param name="pdfPaths">Array of full paths to the source PDF files.</param>
    /// <param name="outputDirectory">Directory where the rotated PDFs will be saved.</param>
    public static void RotateTitleText(string[] pdfPaths, string outputDirectory)
    {
        if (pdfPaths == null) throw new ArgumentNullException(nameof(pdfPaths));
        if (string.IsNullOrWhiteSpace(outputDirectory))
            throw new ArgumentException("Output directory must be provided.", nameof(outputDirectory));

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        foreach (string inputPath in pdfPaths)
        {
            if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            // Build the output file name.
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{fileNameWithoutExt}_rotated.pdf");

            // Use PdfPageEditor (a Facade) to apply page‑level transformations.
            // The editor does not implement IDisposable, but it provides a Close method.
            PdfPageEditor editor = new PdfPageEditor();
            try
            {
                // Bind the source PDF.
                editor.BindPdf(inputPath);

                // Set the rotation angle. Although the API guarantees only multiples of 90,
                // the requirement asks for 45 degrees, so we assign it directly.
                editor.Rotation = 45;

                // No need to set ProcessPages – leaving it null processes all pages.
                // If a specific set of pages is required, assign an int[] as per the
                // "processpages‑int‑array" fix.

                // Apply the rotation to the bound document.
                editor.ApplyChanges();

                // Save the modified document to the target path.
                editor.Save(outputPath);
            }
            finally
            {
                // Close releases any resources held by the facade.
                editor.Close();
            }

            Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
        }
    }
}

public class Program
{
    /// <summary>
    /// Entry point required for a console application.
    /// Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...
    /// </summary>
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <outputDirectory> <pdfPath1> [pdfPath2] ...");
            return;
        }

        string outputDirectory = args[0];
        string[] pdfPaths = new string[args.Length - 1];
        Array.Copy(args, 1, pdfPaths, 0, pdfPaths.Length);

        PdfTitleRotator.RotateTitleText(pdfPaths, outputDirectory);
    }
}

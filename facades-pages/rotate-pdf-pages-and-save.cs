using System;
using System.IO;
using Aspose.Pdf.Facades;

public static class PdfRotationHelper
{
    /// <summary>
    /// Loads a PDF from the provided input stream, rotates all pages by the specified angle,
    /// and writes the result back to a new stream (position set to the beginning).
    /// </summary>
    /// <param name="inputPdfStream">Stream containing the source PDF (must support reading).</param>
    /// <param name="rotationAngle">Rotation angle in degrees (allowed values: 0, 90, 180, 270).</param>
    /// <returns>A stream containing the rotated PDF; the caller is responsible for disposing it.</returns>
    public static Stream RotatePdf(Stream inputPdfStream, int rotationAngle)
    {
        if (inputPdfStream == null) throw new ArgumentNullException(nameof(inputPdfStream));
        if (rotationAngle != 0 && rotationAngle != 90 && rotationAngle != 180 && rotationAngle != 270)
            throw new ArgumentException("Rotation must be 0, 90, 180, or 270 degrees.", nameof(rotationAngle));

        // Ensure the input stream is positioned at the beginning.
        if (inputPdfStream.CanSeek)
            inputPdfStream.Position = 0;

        // Output stream that will receive the edited PDF.
        MemoryStream outputStream = new MemoryStream();

        // Use PdfPageEditor (a SaveableFacade) to edit the PDF.
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            // Bind the source PDF from the input stream.
            editor.BindPdf(inputPdfStream);

            // Apply rotation to all pages.
            editor.Rotation = rotationAngle;

            // Apply the changes before saving.
            editor.ApplyChanges();

            // Save the modified PDF into the output stream.
            editor.Save(outputStream);
        }

        // Reset the output stream position so it can be read from the start.
        if (outputStream.CanSeek)
            outputStream.Position = 0;

        return outputStream;
    }
}

public class Program
{
    /// <summary>
    /// Simple entry point required for a console‑application project.
    /// Demonstrates how to call <see cref="PdfRotationHelper.RotatePdf"/>.
    /// </summary>
    public static void Main(string[] args)
    {
        // If no arguments are supplied, just inform the user and exit.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <exe> <pdf-file-path> [rotation-angle]");
            Console.WriteLine("If rotation-angle is omitted, 90 degrees is used.");
            return;
        }

        string pdfPath = args[0];
        int angle = 90; // default rotation
        if (args.Length > 1 && int.TryParse(args[1], out int parsedAngle))
        {
            angle = parsedAngle;
        }

        // Validate the angle early to surface a clear error message.
        if (angle != 0 && angle != 90 && angle != 180 && angle != 270)
        {
            Console.WriteLine("Rotation angle must be one of 0, 90, 180, 270.");
            return;
        }

        // Open the source PDF for read/write. The stream is closed automatically by the using block.
        using (FileStream sourceStream = new FileStream(pdfPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Rotate the PDF and obtain a new stream containing the result.
            using (Stream rotatedStream = PdfRotationHelper.RotatePdf(sourceStream, angle))
            {
                // Overwrite the original file with the rotated content.
                sourceStream.SetLength(0); // truncate the file
                rotatedStream.CopyTo(sourceStream);
            }
        }

        Console.WriteLine($"PDF rotated by {angle} degrees and saved back to '{pdfPath}'.");
    }
}

using System;
using System.IO;
using Aspose.Pdf; // Document, Rotation enum

// Entry point required for an executable project.
class Program
{
    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // Example usage (can be removed or replaced in production code).
        // -----------------------------------------------------------------
        // The example expects a file named "input.pdf" in the working folder.
        // It rotates the first page 90 degrees clockwise and writes the result
        // to "output.pdf".
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found. Demo skipped.");
            return;
        }

        using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            // Rotate page 1 by 90 degrees.
            PdfProcessor.RotatePdfInStream(inputStream, 1, Rotation.on90);

            // After rotation the stream now contains the new PDF. Write it to a new file.
            inputStream.Position = 0;
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                inputStream.CopyTo(outputStream);
            }
        }

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}

class PdfProcessor
{
    /// <summary>
    /// Loads a PDF from a stream, rotates a page, and writes the result back to the same stream.
    /// The method works with any seek‑able stream (e.g., MemoryStream, NetworkStream that supports seeking).
    /// </summary>
    /// <param name="pdfStream">Stream containing the original PDF. It will be overwritten with the rotated PDF.</param>
    /// <param name="pageNumber">1‑based page index to rotate.</param>
    /// <param name="rotation">Desired rotation (e.g., Rotation.on90, Rotation.on180, Rotation.on270).</param>
    public static void RotatePdfInStream(Stream pdfStream, int pageNumber, Rotation rotation)
    {
        // Ensure the input stream can be read from the beginning.
        if (!pdfStream.CanSeek)
        {
            // If the stream is not seekable, copy it to a temporary MemoryStream.
            using (MemoryStream tempInput = new MemoryStream())
            {
                pdfStream.CopyTo(tempInput);
                tempInput.Position = 0;
                ApplyRotation(tempInput, pdfStream, pageNumber, rotation);
            }
        }
        else
        {
            // Seekable stream: reset position to the start.
            pdfStream.Position = 0;
            ApplyRotation(pdfStream, pdfStream, pageNumber, rotation);
        }
    }

    // Core logic that uses Aspose.Pdf.Document to rotate a page.
    private static void ApplyRotation(Stream input, Stream output, int pageNumber, Rotation rotation)
    {
        // Load the PDF document from the input stream.
        Document doc = new Document(input);

        // Rotate the specified page (pages are 1‑based in Aspose.Pdf).
        doc.Pages[pageNumber].Rotate = rotation;

        // Save the modified PDF into a temporary buffer.
        using (MemoryStream tempOutput = new MemoryStream())
        {
            doc.Save(tempOutput);
            tempOutput.Position = 0;

            // Overwrite the original output stream with the new content.
            if (output.CanSeek)
            {
                output.SetLength(0);          // truncate existing data if possible
                tempOutput.CopyTo(output);
                output.Position = 0;          // reset for any further reading
            }
            else
            {
                // If the output stream cannot be truncated, just write the new data.
                tempOutput.CopyTo(output);
            }
        }
    }
}

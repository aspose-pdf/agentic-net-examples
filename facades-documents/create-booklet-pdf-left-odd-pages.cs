using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required for PdfFileEditor (fully qualified usage is also acceptable)

class Program
{
    static void Main()
    {
        // Input PDF stream – for demonstration we open a file as a stream.
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "booklet_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF from the stream into a Document (required for validation, not for PdfFileEditor).
        using (FileStream inputStream = File.OpenRead(inputPdfPath))
        using (MemoryStream ms = new MemoryStream())
        {
            // Copy the incoming stream to a memory buffer.
            inputStream.CopyTo(ms);
            ms.Position = 0;

            // Validate that the stream contains a readable PDF.
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(ms);
            Console.WriteLine($"PDF loaded – pages: {pdfDoc.Pages.Count}");

            // PdfFileEditor works with file paths, so write the stream to a temporary file.
            string tempFilePath = Path.GetTempFileName();
            try
            {
                File.WriteAllBytes(tempFilePath, ms.ToArray());

                // Prepare source files array for MakeNUp.
                string[] sourceFiles = new string[] { tempFilePath };

                // Create a PdfFileEditor instance.
                Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();

                // MakeNUp creates an N‑up layout (2‑up) which can be used as a simple booklet.
                // The third parameter indicates landscape orientation; true gives a 2‑up layout.
                // There is no direct “left pages odd” option in this API version, so we rely on the default ordering.
                bool success = editor.MakeNUp(sourceFiles, outputPdfPath, true);

                if (success)
                {
                    Console.WriteLine($"Booklet created successfully at '{outputPdfPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to create booklet using MakeNUp.");
                }
            }
            finally
            {
                // Clean up the temporary file.
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
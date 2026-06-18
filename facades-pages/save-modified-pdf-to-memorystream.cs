using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "sample.pdf";

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a memory stream that will hold the resulting PDF
        using (MemoryStream outputStream = new MemoryStream())
        {
            // Instantiate a facade that supports editing and saving to a stream
            // PdfPageEditor inherits from SaveableFacade, which provides Save(Stream)
            PdfPageEditor editor = new PdfPageEditor();

            // Bind the PDF file to the editor (can also use a stream via BindPdf(Stream))
            editor.BindPdf(inputPdf);

            // Example modification: change the zoom factor of the document
            editor.Zoom = 0.75f;

            // Save the modified PDF directly into the memory stream
            editor.Save(outputStream);

            // At this point the MemoryStream contains the PDF data.
            // Reset the position if the stream will be read later.
            outputStream.Position = 0;

            // Example: write the stream length to console
            Console.WriteLine($"Modified PDF saved to memory stream ({outputStream.Length} bytes).");

            // The stream can now be passed to other components without touching the file system.
            // For demonstration, we could write it back to disk (optional):
            // File.WriteAllBytes("modified_output.pdf", outputStream.ToArray());
        }
    }
}
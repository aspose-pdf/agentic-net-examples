using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output PDF path (final file on disk)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use a facade (PdfViewer) to load the PDF and later save it via a stream
        PdfViewer viewer = new PdfViewer();
        try
        {
            // Bind the source PDF to the facade
            viewer.BindPdf(inputPath);

            // Create a writable file stream for the output PDF
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                // Save the PDF document to the stream using the facade's Save method
                viewer.Save(outStream);
                // Flush to ensure all data is written
                outStream.Flush();
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        finally
        {
            // Ensure the facade releases any resources
            viewer.Close();
        }
    }
}
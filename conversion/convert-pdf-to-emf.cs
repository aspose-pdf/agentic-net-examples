using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class PdfToEmfConverter
{
    /// <summary>
    /// Converts each page of a PDF document to a separate EMF image.
    /// </summary>
    /// <param name="pdfPath">Full path to the source PDF file.</param>
    /// <param name="outputDirectory">Folder where EMF files will be saved.</param>
    /// <param name="dpi">Resolution in dots per inch for the raster part of the EMF. Default is 300.</param>
    public static void Convert(string pdfPath, string outputDirectory, int dpi = 300)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDirectory);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create a Resolution object that defines the raster DPI inside the EMF
            Resolution resolution = new Resolution(dpi);

            // Initialize the EMF device with the desired resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for the current page
                string emfPath = Path.Combine(outputDirectory, $"page_{pageNumber}.emf");

                // Open a FileStream for writing the EMF image
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create))
                {
                    // Convert the specific page to EMF and write it to the stream
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as EMF: {emfPath}");
            }
        }
    }

    // Example usage
    static void Main()
    {
        // Adjust these paths as needed
        string pdfFile = @"C:\Data\sample.pdf";
        string outputDir = @"C:\Data\EmfOutput";

        Convert(pdfFile, outputDir);
    }
}
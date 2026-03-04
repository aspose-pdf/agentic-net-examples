using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;   // ImageDevice and derived devices are here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputDir  = "OutputImages";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create a JPEG device for image conversion
                // Set the desired resolution (e.g., 150 DPI)
                JpegDevice jpegDevice = new JpegDevice(150);

                // Set the form presentation mode to Editor.
                // This property exists on ImageDevice (base class of JpegDevice),
                // not on Document.
                jpegDevice.FormPresentationMode = FormPresentationMode.Editor;

                // Convert each page to a separate JPEG file
                for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
                {
                    string imagePath = Path.Combine(outputDir, $"Page_{pageNumber}.jpg");
                    using (FileStream imageStream = File.Create(imagePath))
                    {
                        jpegDevice.Process(pdfDoc.Pages[pageNumber], imageStream);
                    }
                    Console.WriteLine($"Page {pageNumber} saved as JPEG → {imagePath}");
                }
            }

            Console.WriteLine("Conversion completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
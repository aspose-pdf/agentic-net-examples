using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document (1‑based page indexing)
        using (Document pdfDocument = new Document(inputPdf))
        {
            // Define the resolution (dots per inch) for the EMF output
            Resolution resolution = new Resolution(300);

            // Create an EMF device with the specified resolution
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Convert each page to an EMF image
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string emfPath = Path.Combine(outputFolder, $"page_{pageNumber}.emf");

                // Write the EMF image to a file stream
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create))
                {
                    // Process the current page and save it as EMF
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }

                Console.WriteLine($"Saved EMF image: {emfPath}");
            }
        }
    }
}
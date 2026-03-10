using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PCL file and output directory for EMF images
        const string pclPath   = "input.pcl";
        const string outputDir = "EmfPages";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PCL file into a PDF Document using PclLoadOptions
        PclLoadOptions pclLoadOptions = new PclLoadOptions();
        using (Document pdfDoc = new Document(pclPath, pclLoadOptions))
        {
            // Create an EMF device with a desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            // EmfDevice does NOT implement IDisposable, so we instantiate it without a using block
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Build the EMF file name for the current page
                string emfPath = Path.Combine(outputDir, $"page_{pageNum}.emf");

                // Convert the page to EMF and write to the file stream
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create))
                {
                    emfDevice.Process(pdfDoc.Pages[pageNum], emfStream);
                }

                Console.WriteLine($"Page {pageNum} saved as EMF: {emfPath}");
            }
        }

        Console.WriteLine("All pages have been converted to EMF format.");
    }
}

using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PCL file path
        const string pclPath = "input.pcl";
        // Output folder for EMF images
        const string outputFolder = "EmfOutput";

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PCL file as a PDF document using the provided PclLoadOptions rule
        PclLoadOptions pclLoadOptions = new PclLoadOptions();
        using (Document pdfDocument = new Document(pclPath, pclLoadOptions))
        {
            // Create an EMF device with a desired resolution (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate through all pages (1‑based indexing) and convert each to EMF
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string emfPath = Path.Combine(outputFolder, $"page_{pageNumber}.emf");
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the current page to EMF and write to the stream
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }

                Console.WriteLine($"Page {pageNumber} saved as EMF: {emfPath}");
            }
        }

        Console.WriteLine("Conversion from PCL to EMF completed.");
    }
}
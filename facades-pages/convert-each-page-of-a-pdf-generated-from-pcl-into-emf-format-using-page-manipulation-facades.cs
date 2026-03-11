using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Directory that contains the source PCL file and where EMF files will be written
        const string dataDir = @"YOUR_DATA_DIRECTORY";

        // Input PCL file
        string pclPath = Path.Combine(dataDir, "input.pcl");

        // Output file name pattern – {0} will be replaced by the page number
        string emfPattern = Path.Combine(dataDir, "page{0}_out.emf");

        if (!File.Exists(pclPath))
        {
            Console.Error.WriteLine($"PCL file not found: {pclPath}");
            return;
        }

        // Load the PCL file into a PDF Document using PclLoadOptions (PCL is input‑only)
        PclLoadOptions loadOptions = new PclLoadOptions();
        using (Document pdfDoc = new Document(pclPath, loadOptions))
        {
            // Define the resolution for the EMF images (e.g., 300 DPI)
            Resolution resolution = new Resolution(300);

            // EmfDevice converts PDF pages to EMF; it does NOT implement IDisposable, so we instantiate it normally
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Pages collection is 1‑based
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                string emfPath = string.Format(emfPattern, pageNum);
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create))
                {
                    // Convert the current page to EMF and write it to the file stream
                    emfDevice.Process(pdfDoc.Pages[pageNum], emfStream);
                }
            }

            // If future versions of EmfDevice implement IDisposable, you can call Dispose() here safely.
            // emfDevice.Dispose(); // uncomment when IDisposable is supported
        }

        Console.WriteLine("All pages have been converted to EMF.");
    }
}

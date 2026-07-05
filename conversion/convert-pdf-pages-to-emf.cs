using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Resolve a concrete directory relative to the executable location.
        // The folder "Data" will be created automatically if it does not exist.
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string dataDir = Path.GetFullPath(Path.Combine(baseDir, "Data"));
        Directory.CreateDirectory(dataDir);

        // Name of the source PDF file placed inside the Data folder.
        // Change "sample.pdf" to the actual file name you want to convert.
        string pdfFileName = "sample.pdf";
        string pdfPath = Path.Combine(dataDir, pdfFileName);

        // Verify that the PDF file exists before attempting to load it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"PDF file not found: {pdfPath}");
            Console.WriteLine("Please copy the PDF into the Data folder or update the file name.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define the resolution (dots per inch) for the EMF output.
            Resolution resolution = new Resolution(300);

            // EmfDevice does NOT implement IDisposable, so do NOT wrap it in a using statement.
            EmfDevice emfDevice = new EmfDevice(resolution);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                // Build the output file name for each page.
                string emfPath = Path.Combine(dataDir, $"image{pageNumber}_out.emf");

                // Create a file stream for the EMF file inside a using block.
                using (FileStream emfStream = new FileStream(emfPath, FileMode.Create, FileAccess.Write))
                {
                    // Convert the current page to EMF and write it to the stream.
                    emfDevice.Process(pdfDocument.Pages[pageNumber], emfStream);
                }
            }
        }

        Console.WriteLine("Conversion completed. EMF files are saved in: " + dataDir);
    }
}

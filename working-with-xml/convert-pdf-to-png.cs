using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Path to the source PDF file (must exist in the working directory)
        string pdfPath = "input.pdf";

        // If the PDF does not exist, create a simple one‑page document so the demo can run.
        if (!File.Exists(pdfPath))
        {
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a blank page
                // Guard Document.Save against missing GDI+ on non‑Windows platforms
                SaveDocumentSafely(placeholder, pdfPath);
                Console.WriteLine($"Placeholder PDF created at '{pdfPath}'.");
            }
        }

        // Folder where PNG images will be saved
        string outputFolder = "output";

        // Create the output folder if it does not exist
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Load the PDF document
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Define a high resolution (e.g., 300 DPI) for the PNG images
            Resolution resolution = new Resolution(300);

            // Initialize the PNG device with the desired resolution
            PngDevice pngDevice = new PngDevice(resolution);

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string pngPath = Path.Combine(outputFolder, $"page_{pageNumber}.png");

                try
                {
                    // Save each page as a PNG image – this operation may require GDI+ (libgdiplus) on non‑Windows OSes.
                    using (FileStream pngStream = new FileStream(pngPath, FileMode.Create))
                    {
                        pngDevice.Process(pdfDocument.Pages[pageNumber], pngStream);
                    }
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine($"Warning: Unable to render page {pageNumber} to PNG because GDI+ (libgdiplus) is not available on this platform.");
                    Console.WriteLine("If you need PNG conversion on this OS, install libgdiplus or run the program on Windows.");
                    // Optionally break out of the loop – further pages would fail the same way.
                    break;
                }
            }
        }
    }

    // Helper that safely saves a document, handling missing GDI+ on non‑Windows platforms.
    private static void SaveDocumentSafely(Document doc, string path)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            doc.Save(path);
            return;
        }

        try
        {
            doc.Save(path);
        }
        catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
        {
            Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform; the PDF was not saved.");
            Console.WriteLine("Install libgdiplus or run on Windows to enable PDF saving.");
        }
    }

    // Walks the inner‑exception chain to detect a DllNotFoundException (e.g., missing libgdiplus).
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}

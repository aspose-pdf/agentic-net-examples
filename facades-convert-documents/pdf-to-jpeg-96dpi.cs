using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

public class Program
{
    public static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPattern = "image{0}.jpeg";

        // Ensure the source PDF exists. If it does not, create a minimal PDF with a single blank page.
        Document pdfDocument;
        if (File.Exists(inputPdf))
        {
            pdfDocument = new Document(inputPdf);
        }
        else
        {
            // Create an empty PDF with one blank page so the conversion loop can still run.
            pdfDocument = new Document();
            pdfDocument.Pages.Add();
            Console.WriteLine($"Warning: '{inputPdf}' not found. A blank PDF was generated for conversion.");
        }

        // Guard rendering code on platforms where GDI+ (libgdiplus) is not available.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.WriteLine("GDI+ (libgdiplus) is required for PDF to image conversion on non‑Windows platforms. " +
                              "Install libgdiplus or run this code on Windows. Skipping conversion.");
            return;
        }

        // Use a using block to guarantee proper disposal of the Document instance.
        using (pdfDocument)
        {
            // Set the desired resolution (96 DPI) for web‑friendly JPEG output.
            Resolution resolution = new Resolution(96);
            JpegDevice jpegDevice = new JpegDevice(resolution);

            // Convert each page to a separate JPEG file.
            for (int pageNumber = 1; pageNumber <= pdfDocument.Pages.Count; pageNumber++)
            {
                string outputImage = string.Format(outputPattern, pageNumber);
                try
                {
                    using (FileStream imageStream = new FileStream(outputImage, FileMode.Create))
                    {
                        jpegDevice.Process(pdfDocument.Pages[pageNumber], imageStream);
                    }
                    Console.WriteLine($"Page {pageNumber} saved as '{outputImage}'.");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Error: GDI+ (libgdiplus) is missing – cannot render JPEG images on this platform.");
                    // Optionally break out of the loop because further pages will fail as well.
                    break;
                }
            }
        }
    }

    // Helper that walks the inner‑exception chain looking for a DllNotFoundException.
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

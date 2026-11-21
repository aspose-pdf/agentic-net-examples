using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "EMF_Output";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Set resolution (DPI) for EMF output
                const int dpi = 300;
                Resolution resolution = new Resolution(dpi);

                // EmfDevice does NOT implement IDisposable – do NOT wrap it in a using block
                EmfDevice emfDevice = new EmfDevice(resolution);

                // Aspose.Pdf uses 1‑based page indexing
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    Page page = pdfDoc.Pages[i];
                    string outPath = Path.Combine(outputDir, $"Page_{i}.emf");

                    // Render the page preserving vector data into a stream
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                    {
                        emfDevice.Process(page, outStream);
                    }
                }
            }

            Console.WriteLine($"PDF converted to EMF images in '{outputDir}'.");
        }
        catch (TypeInitializationException)
        {
            // EMF conversion relies on GDI+ (Windows only)
            Console.WriteLine("EMF conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (DllNotFoundException)
        {
            Console.WriteLine("Required GDI+ DLL not found. EMF conversion is Windows‑only.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

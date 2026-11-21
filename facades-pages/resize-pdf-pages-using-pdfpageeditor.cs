using System;
using System.IO;
using Aspose.Pdf;

#pragma warning disable NU1902 // Package 'System.Security.Cryptography.Pkcs' moderate vulnerability
#pragma warning disable NU1903 // Package 'System.Security.Cryptography.Pkcs' high vulnerability

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        // Ensure input PDF exists – create a minimal one if missing
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Load PDF bytes
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Process PDF from byte array
        using (var ms = new MemoryStream(pdfBytes))
        using (var doc = new Document(ms))
        {
            double newWidth = 420;   // lower‑left X to upper‑right X
            double newHeight = 595;  // lower‑left Y to upper‑right Y

            foreach (Page page in doc.Pages)
            {
                page.PageInfo.Width = newWidth;
                page.PageInfo.Height = newHeight;
                // Optional: page.PageInfo.IsLandscape = newWidth > newHeight;
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}

#pragma warning restore NU1903
#pragma warning restore NU1902
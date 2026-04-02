using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the input file exists – create an empty PDF if it does not.
        if (!File.Exists(inputPath))
        {
            using (var emptyDoc = new Document())
            {
                emptyDoc.Pages.Add(); // add a blank page so the document is valid
                emptyDoc.Save(inputPath);
                Console.WriteLine($"Created placeholder PDF at '{inputPath}'.");
            }
        }

        using (Document doc = new Document(inputPath))
        {
            // Delete Bates numbering artifacts from the pages.
            // The DeleteBatesNumbering extension works on a PageCollection.
            // Here we remove it from all pages; to target specific pages, iterate and remove as needed.
            doc.Pages.DeleteBatesNumbering();

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"Bates numbering removed. Saved to {outputPath}");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"Bates numbering removed. Saved to {outputPath}");
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("libgdiplus is required for PDF saving on this platform. Save operation skipped.");
                }
            }
        }
    }

    private static bool ContainsDllNotFound(Exception ex)
    {
        Exception current = ex; // removed nullable annotation to avoid CS8632 warning
        while (current != null)
        {
            if (current is DllNotFoundException)
                return true;
            current = current.InnerException;
        }
        return false;
    }
}

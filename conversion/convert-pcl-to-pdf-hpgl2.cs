using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Use a temporary directory that always exists in the sandbox
        string dataDir = Path.GetTempPath();

        // Paths for the sample PCL input and PDF output
        string pclFile = Path.Combine(dataDir, "sample_input.pcl");
        string pdfFile = Path.Combine(dataDir, "output.pdf");

        // ---------------------------------------------------------------------
        // Create a minimal PCL file so the example is self‑contained.
        // The byte sequence below represents a simple PCL reset command (ESC%-12345X).
        // This is sufficient for Aspose.Pdf to open the file without throwing a
        // FileNotFoundException. In a real scenario you would replace this with a
        // genuine PCL/HP‑GL2 source.
        // ---------------------------------------------------------------------
        byte[] minimalPcl = new byte[] { 0x1B, 0x25, 0x2D, 0x31, 0x32, 0x33, 0x34, 0x35, 0x58 };
        File.WriteAllBytes(pclFile, minimalPcl);

        // Initialize PCL load options – HP‑GL/2 vectors are loaded automatically.
        PclLoadOptions pclLoadOptions = new PclLoadOptions();

        try
        {
            // Load the PCL file and convert it to PDF.
            using (Document pdfDocument = new Document(pclFile, pclLoadOptions))
            {
                pdfDocument.Save(pdfFile);
            }

            Console.WriteLine($"PCL file '{pclFile}' has been converted to PDF at '{pdfFile}'.");
        }
        catch (Exception ex)
        {
            // Gracefully report any conversion issues.
            Console.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}

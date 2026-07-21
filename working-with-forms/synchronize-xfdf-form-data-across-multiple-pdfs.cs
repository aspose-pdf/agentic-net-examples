using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the PDF files that need to be synchronized.
        string[] pdfPaths = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Path to the XFDF (XML) file containing the form data.
        const string xfdfPath = "FormData.xfdf";

        // Ensure the XFDF file exists.
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        // Load the XFDF data once into memory to avoid reopening the file for each PDF.
        byte[] xfdfData = File.ReadAllBytes(xfdfPath);

        // Create an output directory for the synchronized PDFs.
        const string outputDir = "SynchronizedOutput";
        Directory.CreateDirectory(outputDir);

        // Process each PDF in parallel to improve performance.
        Parallel.ForEach(pdfPaths, pdfPath =>
        {
            // Verify the source PDF exists.
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF file not found: {pdfPath}");
                return;
            }

            // Load the PDF document (lifecycle: load).
            using (Document pdfDoc = new Document(pdfPath))
            {
                // Import the form field values from the XFDF data (XML format).
                using (MemoryStream xfdfStream = new MemoryStream(xfdfData))
                {
                    // XfdfReader.ReadFields imports field values into the document.
                    XfdfReader.ReadFields(xfdfStream, pdfDoc);
                }

                // Determine the output file path.
                string outputPath = Path.Combine(outputDir, Path.GetFileName(pdfPath));

                // Save the updated PDF (lifecycle: save).
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Synchronized PDF saved: {outputPath}");
            }
        });
    }
}
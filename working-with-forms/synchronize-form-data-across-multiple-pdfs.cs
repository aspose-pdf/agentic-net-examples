using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the PDF files that need to be synchronized.
        string[] pdfInputPaths = new string[]
        {
            "Document1.pdf",
            "Document2.pdf",
            "Document3.pdf"
        };

        // Path to the XML file containing the form data.
        const string xmlDataPath = "FormData.xml";

        // Directory where the updated PDFs will be saved.
        const string outputDirectory = "SyncedOutputs";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDirectory);

        // Process each PDF in parallel to speed up the operation.
        Parallel.ForEach(pdfInputPaths, pdfPath =>
        {
            try
            {
                // Derive output file name.
                string outputFileName = Path.GetFileNameWithoutExtension(pdfPath) + "_Synced.pdf";
                string outputPath = Path.Combine(outputDirectory, outputFileName);

                // Load the PDF document.
                using (Document pdfDoc = new Document(pdfPath))
                {
                    // Bind the XML form data to the document.
                    pdfDoc.BindXml(xmlDataPath);

                    // Save the updated PDF.
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Successfully synchronized: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        });
    }
}
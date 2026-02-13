using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Input FDF file path (change as needed)
        const string fdfPath = "input.fdf";
        // Output XML file path
        const string xmlOutputPath = "output.xml";

        // Verify that the FDF file exists
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"Error: FDF file not found at '{fdfPath}'.");
            return;
        }

        try
        {
            // Create a new blank PDF document (required for importing FDF annotations)
            Document pdfDoc = new Document();

            // Open the FDF file as a stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations (including form data) from the FDF into the PDF document
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting PDF document as XML (Aspose.Pdf internal XML representation)
            // Using the simple save rule without additional options
            pdfDoc.Save(xmlOutputPath);

            Console.WriteLine($"FDF data successfully converted to XML and saved to '{xmlOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during conversion: {ex.Message}");
        }
    }
}
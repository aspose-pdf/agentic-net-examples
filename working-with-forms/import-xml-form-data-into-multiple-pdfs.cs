using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the XML file containing the form data
        const string xmlDataPath = "formData.xml";

        // PDF files that need to receive the same XML data
        string[] pdfInputPaths = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Corresponding output files (could overwrite the originals if desired)
        string[] pdfOutputPaths = { "doc1_filled.pdf", "doc2_filled.pdf", "doc3_filled.pdf" };

        // Verify the XML data file exists
        if (!File.Exists(xmlDataPath))
        {
            Console.Error.WriteLine($"XML data file not found: {xmlDataPath}");
            return;
        }

        // Process each PDF document
        for (int i = 0; i < pdfInputPaths.Length; i++)
        {
            string inputPath = pdfInputPaths[i];
            string outputPath = pdfOutputPaths[i];

            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"PDF file not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the PDF document inside a using block for deterministic disposal
                using (Document pdfDoc = new Document(inputPath))
                {
                    // Bind the XML data to the document.
                    // This imports the XML (e.g., XFA form data) into the PDF.
                    pdfDoc.BindXml(xmlDataPath);

                    // Save the updated PDF. No SaveOptions are needed because we are saving as PDF.
                    pdfDoc.Save(outputPath);
                }

                Console.WriteLine($"Successfully imported XML data into '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}